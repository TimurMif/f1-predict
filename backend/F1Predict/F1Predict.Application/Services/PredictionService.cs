using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с прогнозами
/// </summary>
public class PredictionService(
    IRepository<Prediction> predictionRepository,
    IRepository<User> userRepository,
    IRepository<Driver> driverRepository,
    IRepository<Session> sessionRepository) : IPredictionService
{
    /// <summary>
    /// Возвращает прогнозы, доступные текущему пользователю
    /// </summary>
    public async Task<List<PredictionDto>> GetAllAsync(int? userId, int? sessionId, int currentUserId)
    {
        var predictions = await predictionRepository.GetAllAsync();
        return predictions
            .Where(p => userId is null || p.UserId == userId)
            .Where(p => sessionId is null || p.SessionId == sessionId)
            .Where(p => IsVisibleTo(p, currentUserId))
            .OrderByDescending(p => p.Created)
            .Select(p => p.ToDto())
            .ToList();
    }

    /// <summary>
    /// Возвращает прогноз по его Id
    /// </summary>
    public async Task<PredictionDto?> GetByIdAsync(int id, int currentUserId)
    {
        var prediction = await predictionRepository.GetByIdAsync(id);
        if (prediction is null) return null;

        if (!IsVisibleTo(prediction, currentUserId))
            throw new ForbiddenException("Прогнозы других участников скрыты до закрытия приёма на сессию");

        return prediction.ToDto();
    }

    /// <summary>
    /// Создает новый прогноз
    /// </summary>
    public async Task<PredictionDto> CreateAsync(int userId, PredictionEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(userId)
                   ?? throw new NotFoundException($"Пользователь с Id={userId} не найден");
        var session = await sessionRepository.GetByIdAsync(dto.SessionId)
                      ?? throw new NotFoundException($"Сессия с Id={dto.SessionId} не найдена");

        EnsurePredictionsAccepted(session);
        EnsureDriversDistinct(dto);
        var (topOne, topTwo, topThree) = await GetDriversOrThrow(dto);

        var all = await predictionRepository.GetAllAsync();
        if (all.Any(p => p.UserId == userId && p.SessionId == dto.SessionId))
            throw new ConflictException("Прогноз на эту сессию уже сделан");

        var created = await predictionRepository.AddAsync(dto.ToEntity(user, session, topOne, topTwo, topThree));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет подиум и время круга в своём прогнозе (сессия не меняется)
    /// </summary>
    public async Task<bool> UpdateAsync(int id, int userId, PredictionEditDto dto)
    {
        var prediction = await predictionRepository.GetByIdAsync(id);
        if (prediction is null) return false;

        EnsureOwner(prediction, userId);
        EnsurePredictionsAccepted(prediction.Session);
        EnsureDriversDistinct(dto);
        var (topOne, topTwo, topThree) = await GetDriversOrThrow(dto);

        prediction.DriverTopOneId = topOne.Id;
        prediction.DriverTopOne = topOne;
        prediction.DriverTopTwoId = topTwo.Id;
        prediction.DriverTopTwo = topTwo;
        prediction.DriverTopThreeId = topThree.Id;
        prediction.DriverTopThree = topThree;
        prediction.BestSessionLapTimeMs = dto.BestSessionLapMs;
        prediction.Edited = DateTime.UtcNow;

        return await predictionRepository.UpdateAsync(prediction);
    }

    /// <summary>
    /// Удаляет свой прогноз
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var prediction = await predictionRepository.GetByIdAsync(id);
        if (prediction is null) return false;

        EnsureOwner(prediction, userId);

        if (!IsOpen(prediction.Session))
            throw new ConflictException("Нельзя удалить прогноз после закрытия приёма на сессию");

        return await predictionRepository.DeleteAsync(id);
    }
    
    /// <summary>
    /// Приём прогнозов открыт, пока сессия запланирована и ещё не началась
    /// </summary>
    private static bool IsOpen(Session session) =>
        session.Status == SessionStatus.Scheduled && session.DateStart > DateTime.UtcNow;

    /// <summary>
    /// Свои прогнозы видны всегда, чужие — только после закрытия приёма на сессию
    /// </summary>
    private static bool IsVisibleTo(Prediction prediction, int currentUserId) =>
        prediction.UserId == currentUserId || !IsOpen(prediction.Session);

    private static void EnsurePredictionsAccepted(Session session)
    {
        if (session.Type == SessionType.FreePractice)
            throw new BadRequestException("На свободные практики прогнозы не принимаются");

        if (!IsOpen(session))
            throw new ConflictException("Приём прогнозов на эту сессию закрыт");
    }

    private static void EnsureOwner(Prediction prediction, int userId)
    {
        if (prediction.UserId != userId)
            throw new ForbiddenException("Можно изменять только свои прогнозы");
    }

    private static void EnsureDriversDistinct(PredictionEditDto dto)
    {
        if (dto.DriverTopOneId == dto.DriverTopTwoId ||
            dto.DriverTopOneId == dto.DriverTopThreeId ||
            dto.DriverTopTwoId == dto.DriverTopThreeId)
            throw new BadRequestException("Пилоты в прогнозе должны быть разными");
    }

    private async Task<(Driver TopOne, Driver TopTwo, Driver TopThree)> GetDriversOrThrow(PredictionEditDto dto)
    {
        var topOne = await GetDriverOrThrow(dto.DriverTopOneId);
        var topTwo = await GetDriverOrThrow(dto.DriverTopTwoId);
        var topThree = await GetDriverOrThrow(dto.DriverTopThreeId);
        return (topOne, topTwo, topThree);
    }

    private async Task<Driver> GetDriverOrThrow(int driverId)
    {
        return await driverRepository.GetByIdAsync(driverId)
               ?? throw new NotFoundException($"Пилот с Id={driverId} не найден");
    }
}