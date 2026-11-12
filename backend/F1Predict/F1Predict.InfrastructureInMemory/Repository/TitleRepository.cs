using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

public class TitleRepository(F1PredictContext context) : IRepository<Title>
{
    public Task<List<Title>> GetAllAsync()
    {
        return Task.FromResult(context.Titles.ToList());
    }

    public Task<Title?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Titles.FirstOrDefault(t => t.Id == id));
    }

    public Task<Title> AddAsync(Title entity)
    {
        entity.Id = context.Titles.Select(t => t.Id).DefaultIfEmpty(0).Max() + 1;
        context.Titles.Add(entity);
        
        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId);
        user?.Titles.Add(entity);
        
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(Title entity)
    {
        var existing = context.Titles.FirstOrDefault(t => t.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.Year = entity.Year;
        
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Titles.FirstOrDefault(t => t.Id == id);
        if (entity == null) return Task.FromResult(false);
        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId);
        user?.Titles.Remove(entity);
        
        context.Titles.Remove(entity);
        return Task.FromResult(true);
    }
}