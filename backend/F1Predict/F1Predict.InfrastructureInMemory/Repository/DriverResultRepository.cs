using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с результатами пилотов в сессии
/// </summary>
public class DriverResultRepository(F1PredictContext context) : IRepository<DriverResult>
{
    /// <summary>
    /// Получение всех результатов
    /// </summary>
    public Task<List<DriverResult>> GetAllAsync()
    {
        return Task.FromResult(context.DriverResults.ToList());
    }

    /// <summary>
    /// Получение результата по Id
    /// </summary>
    public Task<DriverResult?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.DriverResults.FirstOrDefault(d => d.Id == id));
    }

    /// <summary>
    /// Добавление нового результата
    /// </summary>
    public Task<DriverResult> AddAsync(DriverResult entity)
    {
        entity.Id = context.DriverResults.Select(r => r.Id).DefaultIfEmpty(0).Max() + 1;
        context.DriverResults.Add(entity);

        var session = context.Sessions.FirstOrDefault(s => s.Id == entity.SessionId);
        session?.Results.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновление существующего результата
    /// </summary>
    public Task<bool> UpdateAsync(DriverResult entity)
    {
        var existing = context.DriverResults.FirstOrDefault(d => d.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.BestLapTimeMs = entity.BestLapTimeMs;
        existing.Position = entity.Position;
        
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаление результата по Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.DriverResults.FirstOrDefault(r => r.Id == id);
        if (entity == null) return Task.FromResult(false);

        var session = context.Sessions.FirstOrDefault(s => s.Id == entity.SessionId);
        session?.Results.Remove(entity);

        context.DriverResults.Remove(entity);
        return Task.FromResult(true);
    }
}