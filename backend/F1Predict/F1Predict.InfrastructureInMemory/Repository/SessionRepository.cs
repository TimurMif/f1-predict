using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

public class SessionRepository(F1PredictContext context) : IRepository<Session>
{
    public Task<List<Session>> GetAllAsync()
    {
        return Task.FromResult(context.Sessions.ToList());
    }

    public Task<Session?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Sessions.FirstOrDefault(s => s.Id == id));
    }

    public Task<Session> AddAsync(Session entity)
    {
        entity.Id = context.Sessions.Select(s => s.Id).DefaultIfEmpty(0).Max() + 1;
        context.Sessions.Add(entity);

        var grandPrix = context.GrandPrix.FirstOrDefault(g => g.Id == entity.GrandPrixId);
        grandPrix?.Sessions.Add(entity);

        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(Session entity)
    {
        var existing = context.Sessions.FirstOrDefault(s => s.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.Name = entity.Name;
        existing.DateEnd = entity.DateEnd;
        existing.DateStart = entity.DateStart;
        existing.Status = entity.Status;
        existing.Type = entity.Type;
        
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Sessions.FirstOrDefault(s => s.Id == id);
        if (entity == null) return Task.FromResult(false);

        // Прогнозы — пользовательские данные: молча не удаляем, запрещаем удаление сессии
        if (context.Predictions.Any(p => p.SessionId == id)) return Task.FromResult(false);

        // Результаты — «дети» сессии: каскадное удаление
        context.DriverResults.RemoveAll(r => r.SessionId == id);

        var grandPrix = context.GrandPrix.FirstOrDefault(g => g.Id == entity.GrandPrixId);
        grandPrix?.Sessions.Remove(entity);

        context.Sessions.Remove(entity);
        return Task.FromResult(true);
    }
}