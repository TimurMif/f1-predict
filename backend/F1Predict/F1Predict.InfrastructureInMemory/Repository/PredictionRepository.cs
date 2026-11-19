using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с прогнозами
/// </summary>
public class PredictionRepository(F1PredictContext context) : IRepository<Prediction>
{
    /// <summary>
    /// Возвращает все прогнозы
    /// </summary>
    public Task<List<Prediction>> GetAllAsync()
    {
        return Task.FromResult(context.Predictions.ToList());
    }

    /// <summary>
    /// Возвращает прогноз по Id
    /// </summary>
    public Task<Prediction?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Predictions.FirstOrDefault(p => p.Id == id));
    }

    /// <summary>
    /// Добавляем новый прогноз
    /// </summary>
    public Task<Prediction> AddAsync(Prediction entity)
    {
        entity.Id = context.Predictions.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1;
        context.Predictions.Add(entity);

        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId);
        user?.Predictions.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляем прогноз
    /// </summary>
    public Task<bool> UpdateAsync(Prediction entity)
    {
        var existing = context.Predictions.FirstOrDefault(p => p.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.BestSessionLapTimeMs = entity.BestSessionLapTimeMs;
        existing.DriverTopOne =  entity.DriverTopOne;
        existing.DriverTopOneId = entity.DriverTopOneId;
        existing.DriverTopTwo = entity.DriverTopTwo;
        existing.DriverTopTwoId = entity.DriverTopTwoId;
        existing.DriverTopThree = entity.DriverTopThree;
        existing.DriverTopThreeId = entity.DriverTopThreeId;
        existing.Edited = entity.Edited;
        
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляем прогноз по Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Predictions.FirstOrDefault(p => p.Id == id);
        if (entity == null) return Task.FromResult(false);

        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId);
        user?.Predictions.Remove(entity);

        context.Predictions.Remove(entity);
        return Task.FromResult(true);
    }
}