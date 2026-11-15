using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с титулами пользователей
/// </summary>
public class TitleService(IRepository<Title> titleRepository, IRepository<User> userRepository) : ITitleService
{
    /// <summary>
    /// Возвращает список всех титулов пользователей
    /// </summary>
    public async Task<List<TitleDto>> GetAllAsync()
    {
        var titles = await titleRepository.GetAllAsync();
        return titles.Select(t => t.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает титул по его Id
    /// </summary>
    public async Task<TitleDto?> GetByIdAsync(int id)
    {
        var title = await titleRepository.GetByIdAsync(id);
        return title?.ToDto();
    }
    
    /// <summary>
    /// Создает новый титул
    /// </summary>
    public async Task<TitleDto?> CreateAsync(TitleEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(dto.UserId);
        if (user is null) return null;

        EnsureYearNotFuture(dto.Year);
        await EnsureTitleUnique(dto.UserId, dto.Year, excludeId: null);

        var created = await titleRepository.AddAsync(dto.ToEntity(user));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего титула
    /// </summary>
    public async Task<bool> UpdateAsync(int id, TitleEditDto dto)
    {
        var title = await titleRepository.GetByIdAsync(id);
        if (title is null) return false;

        EnsureYearNotFuture(dto.Year);
        await EnsureTitleUnique(title.UserId, dto.Year, excludeId: id);

        title.Year = dto.Year;
        return await titleRepository.UpdateAsync(title);
    }

    /// <summary>
    /// Удаляет титул по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return titleRepository.DeleteAsync(id);
    }
    
    private static void EnsureYearNotFuture(int year)
    {
        if (year > DateTime.UtcNow.Year)
            throw new ConflictException("Нельзя выдать титул за будущий сезон");
    }
    
    private async Task EnsureTitleUnique(int userId, int year, int? excludeId)
    {
        var all = await titleRepository.GetAllAsync();
        if (all.Any(t => t.UserId == userId && t.Year == year && t.Id != excludeId))
            throw new ConflictException($"У пользователя уже есть титул за {year} сезон");
    }
}