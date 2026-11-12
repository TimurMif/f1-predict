using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

public class GrandPrixRepository(F1PredictContext context) : IRepository<GrandPrix>
{
    public Task<List<GrandPrix>> GetAllAsync()
    {
        return Task.FromResult(context.GrandPrix.ToList());
    }

    public Task<GrandPrix?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.GrandPrix.FirstOrDefault(g => g.Id == id));
    }

    public Task<GrandPrix> AddAsync(GrandPrix entity)
    {
        entity.Id = context.GrandPrix.Select(g => g.Id).DefaultIfEmpty(0).Max() + 1;
        context.GrandPrix.Add(entity);
        
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(GrandPrix entity)
    {
        var existing = context.GrandPrix.FirstOrDefault(g => g.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.Name = entity.Name;
        existing.CircuitName = entity.CircuitName;
        existing.Country = entity.Country;
        existing.RoundNumber = entity.RoundNumber;
        existing.Year = entity.Year;
        
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.GrandPrix.FirstOrDefault(g => g.Id == id);
        if (entity == null) return Task.FromResult(false);
        if (context.Sessions.Any(s => s.GrandPrixId == id)) return Task.FromResult(false);
        
        context.GrandPrix.Remove(entity);
        return Task.FromResult(true);
    }
}