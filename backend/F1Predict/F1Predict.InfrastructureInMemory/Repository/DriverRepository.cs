using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

public class DriverRepository(F1PredictContext context) : IRepository<Driver>
{
    public Task<List<Driver>> GetAllAsync()
    {
        return Task.FromResult(context.Drivers.ToList());
    }

    public Task<Driver?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Drivers.FirstOrDefault(d => d.Id == id));
    }

    public Task<Driver> AddAsync(Driver entity)
    {
        entity.Id = context.Drivers.Select(d => d.Id).DefaultIfEmpty(0).Max() + 1;
        context.Drivers.Add(entity);
        
        return Task.FromResult(entity);
    }

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