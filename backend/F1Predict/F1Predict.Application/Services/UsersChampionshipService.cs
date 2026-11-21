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
    IRepository<UserInChampionship> userInChampionshipRepository,
    IRepository<User> userRepository) : IUsersChampionshipService
{
    private const int MaxChampionshipsPerSeason = 1;

    /// <summary>
    /// Возвращает чемпионаты текущего пользователя
    /// </summary>
    public async Task<List<UsersChampionshipDto>> GetAllAsync(int currentUserId)
    {
        var championships = await usersChampionshipRepository.GetAllAsync();
        return championships
            .Where(c => IsMemberOrHost(c, currentUserId))
            .Select(c => c.ToDto())
            .ToList();
    }

    /// <summary>
    /// Возвращает чемпионат по его Id
    /// </summary>
    public async Task<UsersChampionshipDto?> GetByIdAsync(int id, int currentUserId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return null;

        EnsureMemberOrHost(championship, currentUserId);
        return championship.ToDto();
    }

    /// <summary>
    /// Возвращает таблицу участников чемпионата
    /// </summary>
    public async Task<List<UserInChampionshipDto>> GetStandingsAsync(int id, int currentUserId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id)
                           ?? throw new NotFoundException($"Чемпионат с Id={id} не найден");

        EnsureMemberOrHost(championship, currentUserId);

        return championship.Users
            .OrderByDescending(u => u.TotalPoints)
            .ThenBy(u => u.JoinedAt)
            .Select(u => u.ToDto())
            .ToList();
    }

    /// <summary>
    /// Создает новый чемпионат пользователей
    /// </summary>
    public async Task<UsersChampionshipDto> CreateAsync(int userId, UsersChampionshipEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(userId)
                   ?? throw new NotFoundException($"Пользователь с Id={userId} не найден");

        if (user.UserInChampionship is not null)
            throw new ConflictException("Вы уже состоите в чемпионате — сначала выйдите из текущего");

        var all = await usersChampionshipRepository.GetAllAsync();
        if (all.Count(c => c.CreatedByUserId == userId && c.Year == dto.Year) >= MaxChampionshipsPerSeason)
            throw new ConflictException("Лимит чемпионатов на сезон исчерпан");

        var created = await usersChampionshipRepository.AddAsync(
            dto.ToEntity(user, GenerateUniqueInviteCode(all)));

        // создатель автоматически становится участником своего чемпионата
        await userInChampionshipRepository.AddAsync(new UserInChampionship
        {
            UserId = user.Id,
            User = user,
            ChampionshipId = created.Id,
            Championship = created,
            TotalPoints = 0,
            JoinedAt = DateTime.UtcNow
        });

        return created.ToDto();
    }

    /// <summary>
    /// Обновляем данные существующего чемпионата
    /// </summary>
    public async Task<bool> UpdateAsync(int id, int userId, UsersChampionshipEditDto dto)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return false;

        EnsureHost(championship, userId);

        championship.Year = dto.Year;
        return await usersChampionshipRepository.UpdateAsync(championship);
    }

    /// <summary>
    /// Получение кода приглашения (только создателю)
    /// </summary>
    public async Task<string?> GetInviteCodeAsync(int id, int userId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return null;

        EnsureHost(championship, userId);
        return championship.InviteCode;
    }

    /// <summary>
    /// Удаляет чемпионат по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(id);
        if (championship is null) return false;

        EnsureHost(championship, userId);
        return await usersChampionshipRepository.DeleteAsync(id);
    }

    private static bool IsMemberOrHost(UsersChampionship championship, int userId) =>
        championship.CreatedByUserId == userId || championship.Users.Any(u => u.UserId == userId);

    private static void EnsureMemberOrHost(UsersChampionship championship, int userId)
    {
        if (!IsMemberOrHost(championship, userId))
            throw new ForbiddenException("Чемпионат доступен только его участникам");
    }

    private static void EnsureHost(UsersChampionship championship, int userId)
    {
        if (championship.CreatedByUserId != userId)
            throw new ForbiddenException("Это действие доступно только создателю чемпионата");
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