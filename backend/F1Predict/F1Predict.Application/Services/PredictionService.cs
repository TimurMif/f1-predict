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
    /// Возвращает список всех прогнозов
    /// </summary>
    public async Task<List<PredictionDto>> GetAllAsync()
    {
        var predictions = await predictionRepository.GetAllAsync();
        return predictions.Select(p => p.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает прогноз по его Id
    /// </summary>
    public async Task<PredictionDto?> GetByIdAsync(int id)
    {
        var prediction = await predictionRepository.GetByIdAsync(id);
        return prediction?.ToDto();
    }

    /// <summary>
    /// Создает новый прогноз
    /// </summary>
    public async Task<PredictionDto?> CreateAsync(int userId, PredictionEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null) return null;

        var session = await sessionRepository.GetByIdAsync(dto.SessionId);
        if (session is null) return null;

        if (session.Status != SessionStatus.Scheduled || session.DateStart <= DateTime.UtcNow)
            throw new InvalidOperationException("Приём прогнозов на эту сессию закрыт");

        var topOne = await driverRepository.GetByIdAsync(dto.DriverTopOneId);
        var topTwo = await driverRepository.GetByIdAsync(dto.DriverTopTwoId);
        var topThree = await driverRepository.GetByIdAsync(dto.DriverTopThreeId);
        if (topOne is null || topTwo is null || topThree is null) return null;

        if (dto.DriverTopOneId == dto.DriverTopTwoId ||
            dto.DriverTopOneId == dto.DriverTopThreeId ||
            dto.DriverTopTwoId == dto.DriverTopThreeId)
            throw new InvalidOperationException("Пилоты в прогнозе должны быть разными");

        if ((await predictionRepository.GetAllAsync()).Any(p => p.UserId == userId && p.SessionId == dto.SessionId))
            throw new InvalidOperationException("Прогноз на эту сессию уже сделан");

        var created = await predictionRepository.AddAsync(dto.ToEntity(user, session, topOne, topTwo, topThree));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего прогноза
    /// </summary>
    public async Task<bool> UpdateAsync(int id, int userId, PredictionEditDto dto)
    {
        var prediction = await predictionRepository.GetByIdAsync(id);
        if (prediction is null) return false;
        if (prediction.UserId != userId) return false;   // чужой прогноз не редактируем

        if (prediction.Session.Status != SessionStatus.Scheduled || prediction.Session.DateStart <= DateTime.UtcNow)
            throw new InvalidOperationException("Приём прогнозов на эту сессию закрыт");

        var topOne = await driverRepository.GetByIdAsync(dto.DriverTopOneId);
        var topTwo = await driverRepository.GetByIdAsync(dto.DriverTopTwoId);
        var topThree = await driverRepository.GetByIdAsync(dto.DriverTopThreeId);
        if (topOne is null || topTwo is null || topThree is null) return false;

        if (dto.DriverTopOneId == dto.DriverTopTwoId ||
            dto.DriverTopOneId == dto.DriverTopThreeId ||
            dto.DriverTopTwoId == dto.DriverTopThreeId)
            throw new InvalidOperationException("Пилоты в прогнозе должны быть разными");

        prediction.DriverTopOneId = topOne.Id;  prediction.DriverTopOne = topOne;
        prediction.DriverTopTwoId = topTwo.Id;  prediction.DriverTopTwo = topTwo;
        prediction.DriverTopThreeId = topThree.Id; prediction.DriverTopThree = topThree;
        prediction.BestSessionLapTimeMs = dto.BestSessionLapMs;
        prediction.Edited = DateTime.UtcNow;

        return await predictionRepository.UpdateAsync(prediction);
    }

    /// <summary>
    /// Удаляет прогноз по Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var prediction = await predictionRepository.GetByIdAsync(id);
        if (prediction is null) return false;
        if (prediction.UserId != userId) return false;

        if (prediction.Session.Status != SessionStatus.Scheduled || prediction.Session.DateStart <= DateTime.UtcNow)
            throw new InvalidOperationException("Нельзя удалить прогноз после старта сессии");

        return await predictionRepository.DeleteAsync(id);
    }
}