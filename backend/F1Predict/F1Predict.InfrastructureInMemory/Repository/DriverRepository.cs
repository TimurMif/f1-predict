using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с пилотами
/// </summary>
public class DriverRepository(F1PredictContext context) : IRepository<Driver>
{
    /// <summary>
    /// Возвращает всех пилотов
    /// </summary>
    public Task<List<Driver>> GetAllAsync()
    {
        return Task.FromResult(context.Drivers.ToList());
    }

    /// <summary>
    /// Получение пилота по его id
    /// </summary>
    public Task<Driver?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Drivers.FirstOrDefault(d => d.Id == id));
    }

    /// <summary>
    /// Добавляем нового пилота
    /// </summary>
    public Task<Driver> AddAsync(Driver entity)
    {
        entity.Id = context.Drivers.Select(d => d.Id).DefaultIfEmpty(0).Max() + 1;
        context.Drivers.Add(entity);
        
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновление данных существующего пилота
    /// </summary>
    public Task<bool> UpdateAsync(Driver entity)
    {
        var existing = context.Drivers.FirstOrDefault(d => d.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.FirstName = entity.FirstName;
        existing.LastName = entity.LastName;
        existing.Nationality = entity.Nationality;
        existing.ConstructorId = entity.ConstructorId;
        existing.Points =  entity.Points;
        existing.Constructor = entity.Constructor;
        existing.Number = entity.Number;
        
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаление пилота по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Drivers.FirstOrDefault(d => d.Id == id);
        if (entity == null) return Task.FromResult(false);
        if (context.DriverResults.Any(r => r.DriverId == id)) return Task.FromResult(false);
        if (context.Predictions.Any(p => p.DriverTopOneId == id || p.DriverTopTwoId == id || p.DriverTopThreeId == id))
            return Task.FromResult(false);
        
        context.Drivers.Remove(entity);
        return Task.FromResult(true);
    }
}