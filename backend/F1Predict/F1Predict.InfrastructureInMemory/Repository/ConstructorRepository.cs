using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

public class ConstructorRepository(F1PredictContext context) : IRepository<Constructor>
{
    public Task<List<Constructor>> GetAllAsync()
    {
        return Task.FromResult(context.Constructors.ToList());
    }

    public Task<Constructor?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Constructors.FirstOrDefault(c => c.Id == id));
    }

    public Task<Constructor> AddAsync(Constructor entity)
    {
        entity.Id = context.Constructors.Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;
        context.Constructors.Add(entity);
        
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(Constructor entity)
    {
        var existing = context.Constructors.FirstOrDefault(c => c.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.Points = entity.Points;
        existing.Name = entity.Name;
        existing.Nationality = entity.Nationality;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Constructors.FirstOrDefault(c => c.Id == id);
        if (entity == null) return Task.FromResult(false);
        if (context.Drivers.Any(d => d.ConstructorId == id)) return Task.FromResult(false);
        
        context.Constructors.Remove(entity);
        return Task.FromResult(true);
    }
}