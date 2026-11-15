using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с результатами пилотов
/// </summary>
public class DriverResultService(
    IRepository<DriverResult> driverResultRepository,
    IRepository<Session> sessionRepository,
    IRepository<Driver> driverRepository) : IDriverResultService
{
    /// <summary>
    /// Возвращает всех результаты пилотов
    /// </summary>
    public async Task<List<DriverResultDto>> GetAllAsync()
    {
        var driverResults = await driverResultRepository.GetAllAsync();
        return driverResults.Select(d => d.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает результат пилота по Id
    /// </summary>
    public async Task<DriverResultDto?> GetByIdAsync(int id)
    {
        var driverResult = await driverResultRepository.GetByIdAsync(id);
        return driverResult?.ToDto();
    }

    /// <summary>
    /// Создает новый результат пилота
    /// </summary>
    public async Task<DriverResultDto?> CreateAsync(DriverResultEditDto dto)
    {
        var session = await sessionRepository.GetByIdAsync(dto.SessionId);
        if (session is null) return null;
        var driver = await driverRepository.GetByIdAsync(dto.DriverId);
        if (driver is null) return null;

        if (session.Status != SessionStatus.Completed)
            throw new InvalidOperationException("Результаты вносятся только после завершения сессии");

        var all = await driverResultRepository.GetAllAsync();
        if (all.Any(r => r.SessionId == dto.SessionId && r.DriverId == dto.DriverId))
            throw new InvalidOperationException("Результат этого пилота в данной сессии уже существует");
        if (all.Any(r => r.SessionId == dto.SessionId && r.Position == dto.Position))
            throw new InvalidOperationException("Эта позиция в данной сессии уже занята");

        var created = await driverResultRepository.AddAsync(dto.ToEntity(session, driver));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего результата пилота
    /// </summary>
    public async Task<bool> UpdateAsync(int id, DriverResultEditDto dto)
    {
        var result = await driverResultRepository.GetByIdAsync(id);
        if (result is null) return false;

        var all = await driverResultRepository.GetAllAsync();
        if (all.Any(r => r.SessionId == result.SessionId && r.Position == dto.Position && r.Id != id))
            throw new InvalidOperationException("Эта позиция в данной сессии уже занята");

        result.Position = dto.Position;
        result.BestLapTimeMs = dto.BestLapTimeMs;
        return await driverResultRepository.UpdateAsync(result);
    }

    /// <summary>
    /// Удаляет результат пилота по Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return driverResultRepository.DeleteAsync(id);
    }
}