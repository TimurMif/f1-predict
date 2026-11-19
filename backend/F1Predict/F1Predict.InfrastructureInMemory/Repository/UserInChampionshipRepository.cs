using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с записью пользователя в чемпионате
/// </summary>
public class UserInChampionshipRepository(F1PredictContext context) : IRepository<UserInChampionship>
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public Task<List<UserInChampionship>> GetAllAsync()
    {
        return Task.FromResult(context.UserInChampionships.ToList());
    }

    /// <summary>
    /// Возвращает запись по Id
    /// </summary>
    public Task<UserInChampionship?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.UserInChampionships.FirstOrDefault(u => u.Id == id));
    }

    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    public Task<UserInChampionship> AddAsync(UserInChampionship entity)
    {
        // один активный чемпионат на пользователя.
        if (context.UserInChampionships.Any(p => p.UserId == entity.UserId))
            throw new ConflictException("Пользователь уже состоит в чемпионате");

        entity.Id = context.UserInChampionships.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1;
        context.UserInChampionships.Add(entity);

        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId);
        if (user != null) user.UserInChampionship = entity;

        var championship = context.UsersChampionships.FirstOrDefault(c => c.Id == entity.ChampionshipId);
        championship?.Users.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    public Task<bool> UpdateAsync(UserInChampionship entity)
    {
        var existing = context.UserInChampionships.FirstOrDefault(u => u.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.TotalPoints = entity.TotalPoints;
        
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет запись по Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.UserInChampionships.FirstOrDefault(p => p.Id == id);
        if (entity == null) return Task.FromResult(false);

        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId);
        user?.UserInChampionship = null;

        var championship = context.UsersChampionships.FirstOrDefault(c => c.Id == entity.ChampionshipId);
        championship?.Users.Remove(entity);

        context.UserInChampionships.Remove(entity);
        return Task.FromResult(true);
    }
}