using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

public class UsersChampionshipRepository(F1PredictContext context) : IRepository<UsersChampionship>
{
    public Task<List<UsersChampionship>> GetAllAsync()
    {
        return Task.FromResult(context.UsersChampionships.ToList());
    }

    public Task<UsersChampionship?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.UsersChampionships.FirstOrDefault(u => u.Id == id));
    }

    public Task<UsersChampionship> AddAsync(UsersChampionship entity)
    {
        entity.Id = context.UsersChampionships.Select(u => u.Id).DefaultIfEmpty(0).Max() + 1;
        context.UsersChampionships.Add(entity);
        
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(UsersChampionship entity)
    {
        var existing = context.UsersChampionships.FirstOrDefault(u => u.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.Year = entity.Year;
        existing.InviteCode =  entity.InviteCode;
        
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.UsersChampionships.FirstOrDefault(c => c.Id == id);
        if (entity == null) return Task.FromResult(false);

        // Каскад: удаляем участников и освобождаем пользователей
        foreach (var p in context.UserInChampionships.Where(p => p.ChampionshipId == id).ToList())
        {
            var user = context.Users.FirstOrDefault(u => u.Id == p.UserId);
            user?.UserInChampionship = null;
        }
        context.UserInChampionships.RemoveAll(p => p.ChampionshipId == id);

        context.UsersChampionships.Remove(entity);
        return Task.FromResult(true);
    }
}