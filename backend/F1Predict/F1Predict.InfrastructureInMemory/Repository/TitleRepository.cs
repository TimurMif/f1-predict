using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Context;
using F1Predict.Domain.Models;

namespace F1Predict.Infrastructure.Repository;

/// <summary>
/// Репозиторий для работы с титулами
/// </summary>
public class TitleRepository(F1PredictContext context) : IRepository<Title>
{
    /// <summary>
    /// Возвращает все титулы
    /// </summary>
    public Task<List<Title>> GetAllAsync()
    {
        return Task.FromResult(context.Titles.ToList());
    }

    /// <summary>
    /// Возвращает титул по Id
    /// </summary>
    public Task<Title?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Titles.FirstOrDefault(t => t.Id == id));
    }

    /// <summary>
    /// Добавляет новый титул
    /// </summary>
    public Task<Title> AddAsync(Title entity)
    {
        var user = context.Users.FirstOrDefault(u => u.Id == entity.UserId)
                   ?? throw new NotFoundException($"Пользователь с Id={entity.UserId} не найден");

        entity.Id = context.Titles.Select(t => t.Id).DefaultIfEmpty(0).Max() + 1;
        context.Titles.Add(entity);
        user.Titles.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет существующий титул
    /// </summary>
    public Task<bool> UpdateAsync(Title entity)
    {
        var existing = context.Titles.FirstOrDefault(t => t.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.Year = entity.Year;
        
        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет титул по Id
    /// </summary>
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