using F1Predict.Application.Exceptions;
using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис участия пользователей в чемпионатах
/// </summary>
public class UserInChampionshipService(
    IRepository<UserInChampionship> userInChampionshipRepository,
    IRepository<UsersChampionship> usersChampionshipRepository,
    IRepository<User> userRepository) : IUserInChampionshipService
{
    /// <summary>
    /// Вступление в чемпионат по пригласительному коду
    /// </summary>
    public async Task<UserInChampionshipDto> JoinAsync(int userId, UserInChampionshipEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(userId)
                   ?? throw new NotFoundException($"Пользователь с Id={userId} не найден");

        if (user.UserInChampionship is not null)
            throw new ConflictException("Вы уже состоите в чемпионате — сначала выйдите из текущего");

        var code = dto.InviteCode.Trim().ToUpperInvariant();
        var championship = (await usersChampionshipRepository.GetAllAsync())
                               .FirstOrDefault(c => c.InviteCode == code)
                           ?? throw new NotFoundException("Чемпионат с таким кодом приглашения не найден");

        var created = await userInChampionshipRepository.AddAsync(dto.ToEntity(user, championship));
        return created.ToDto();
    }
    
    /// <summary>
    /// Исключение участника из чемпионата
    /// </summary>
    public async Task<bool> KickAsync(int championshipId, int targetUserId, int currentUserId)
    {
        var championship = await usersChampionshipRepository.GetByIdAsync(championshipId);
        if (championship is null) return false;

        if (championship.CreatedByUserId != currentUserId)
            throw new ForbiddenException("Исключать участников может только создатель чемпионата");

        if (targetUserId == championship.CreatedByUserId)
            throw new ConflictException("Создателя нельзя исключить — удалите чемпионат");

        var all = await userInChampionshipRepository.GetAllAsync();
        var membership = all.FirstOrDefault(m => m.ChampionshipId == championshipId && m.UserId == targetUserId);
        if (membership is null) return false;

        return await userInChampionshipRepository.DeleteAsync(membership.Id);
    }

    /// <summary>
    /// Выход из чемпионата
    /// </summary>
    public async Task<bool> LeaveAsync(int championshipId, int userId)
    {
        var all = await userInChampionshipRepository.GetAllAsync();
        var membership = all.FirstOrDefault(m => m.ChampionshipId == championshipId && m.UserId == userId);
        if (membership is null) return false;

        var championship = await usersChampionshipRepository.GetByIdAsync(championshipId);
        if (championship?.CreatedByUserId == userId)
            throw new ConflictException("Создатель не может выйти из своего чемпионата — удалите чемпионат");

        return await userInChampionshipRepository.DeleteAsync(membership.Id);
    }
}