using F1Predict.Context;
using F1Predict.Application.Interfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с Базой данных пользователей
/// </summary>
public class UserRepository(F1PredictContext context) : IRepository<User>
{
    /// <summary>
    /// Возвращает всех пользователей
    /// </summary>
    public Task<List<User>> GetAllAsync()
    {
        return Task.FromResult(context.Users.ToList());
    }

    /// <summary>
    /// Возвращает пользователя по Id
    /// </summary>
    public Task<User?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Users.FirstOrDefault(u => u.Id == id));
    }

    /// <summary>
    /// Добавляет нового пользователя
    /// </summary>
    public Task<User> AddAsync(User entity)
    {
        entity.Id = context.Users.Select(u => u.Id).DefaultIfEmpty(0).Max() + 1;
        context.Users.Add(entity);
        
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет пользователя
    /// </summary>
    public Task<bool> UpdateAsync(User entity)
    {
        var existing = context.Users.FirstOrDefault(u => u.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.FirstName = entity.FirstName;
        existing.LastName = entity.LastName;
        existing.VkId = entity.VkId;
        existing.TelegramId = entity.TelegramId;
        
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет пользователя по Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Users.FirstOrDefault(c => c.Id == id);
        if (entity == null) return Task.FromResult(false);
        
        var created = context.UsersChampionships.Where(c => c.CreatedByUserId == id).ToList();
        foreach (var champ in created)
        {
            foreach (var p in context.UserInChampionships.Where(p => p.ChampionshipId == champ.Id).ToList())
            {
                var participant = context.Users.FirstOrDefault(u => u.Id == p.UserId);
                if (participant != null) participant.UserInChampionship = null;
            }
            context.UserInChampionships.RemoveAll(p => p.ChampionshipId == champ.Id);
        }
        context.UsersChampionships.RemoveAll(c => c.CreatedByUserId == id);
        
        foreach (var p in context.UserInChampionships.Where(p => p.UserId == id).ToList())
        {
            var champ = context.UsersChampionships.FirstOrDefault(c => c.Id == p.ChampionshipId);
            champ?.Users.Remove(p);
        }
        
        context.UserInChampionships.RemoveAll(p => p.UserId == id);
        context.Predictions.RemoveAll(p => p.UserId == id);
        context.Titles.RemoveAll(t => t.UserId == id);
        
        context.Users.Remove(entity);
        return Task.FromResult(true);
    }
}