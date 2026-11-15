using System.Security.Cryptography;
using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с чемпионатами пользователей
/// </summary>
public class UsersChampionshipService(
    IRepository<UsersChampionship> usersChampionshipRepository,
    IRepository<User> userRepository) : IUsersChampionshipService
{
    private const int MaxChampionshipsPerSeason = 1;

    /// <summary>
    /// Возвращает список всех чемпионатов пользователей
    /// </summary>
    public async Task<List<UsersChampionshipDto>> GetAllAsync()
    {
        var championships = await usersChampionshipRepository.GetAllAsync();
        return championships.Select(c => c.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает чемпионат по его Id
    /// </summary>
    public async Task<UsersChampionshipDto?> GetByIdAsync(int id)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        return championship?.ToDto();
    }

    /// <summary>
    /// Создает новый чемпионат пользователей
    /// </summary>
    public async Task<UsersChampionshipDto?> CreateAsync(int userId, UsersChampionshipEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null) return null;

        var all = await usersChampionshipRepository.GetAllAsync();

        if (all.Count(c => c.CreatedByUserId == userId && c.Year == dto.Year) >= MaxChampionshipsPerSeason)
            throw new ConflictException("Лимит чемпионатов на сезон исчерпан");

        var created = await usersChampionshipRepository.AddAsync(dto.ToEntity(user, GenerateUniqueInviteCode(all)));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляем данные существующего чемпионата
    /// </summary>
    public async Task<bool> UpdateAsync(int id, int userId, UsersChampionshipEditDto dto)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return false;
        if (championship.CreatedByUserId != userId) return false;

        championship.Year = dto.Year;   
        return await usersChampionshipRepository.UpdateAsync(championship);
    }
    
    /// <summary>
    /// Получение кода приглашения
    /// </summary>
    public async Task<string?> GetInviteCodeAsync(int id, int userId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return null;
        if (championship.CreatedByUserId != userId) return null;   // чужой код не палим
        return championship.InviteCode;
    }

    /// <summary>
    /// Удаляет чемпионат по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return false;
        if (championship.CreatedByUserId != userId) return false;

        return await usersChampionshipRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 6-значный код из криптографического RNG, уникальный среди существующих.
    /// </summary>
    private static string GenerateUniqueInviteCode(List<UsersChampionship> existing)
    {
        string code;
        do
        {
            code = RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
        } while (existing.Any(c => c.InviteCode == code));

        return code;
    }
}