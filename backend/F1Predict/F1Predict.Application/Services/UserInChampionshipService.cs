using F1Predict.Application.Interfaces;
using F1Predict.Application.Mapping;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с записями пользователей в чемпионате
/// </summary>
public class UserInChampionshipService(
    IRepository<UserInChampionship> userInChampionshipRepository,
    IRepository<UsersChampionship> usersChampionshipRepository,
    IRepository<User> userRepository) : IUserInChampionshipService
{
    /// <summary>
    /// Возвращает список записей пользователей
    /// </summary>
    public async Task<List<UserInChampionshipDto>> GetAllAsync()
    {
        var userInChampionships = await userInChampionshipRepository.GetAllAsync();
        return userInChampionships.Select(u => u.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает запись пользователя по ее Id
    /// </summary>
    public async Task<UserInChampionshipDto?> GetByIdAsync(int id)
    {
        var userInChampionship = await userInChampionshipRepository.GetByIdAsync(id);
        return userInChampionship?.ToDto();
    }

    /// <summary>
    /// Создание новой записи пользователя
    /// </summary>
    public async Task<UserInChampionshipDto?> CreateAsync(int userId, UserInChampionshipEditDto dto)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null) return null;

        if (user.UserInChampionship is not null)
            throw new InvalidOperationException("Вы уже состоите в чемпионате — сначала выйдите из текущего");

        var championship = (await usersChampionshipRepository.GetAllAsync())
            .FirstOrDefault(c => c.InviteCode == dto.InviteCode.ToUpperInvariant());
        if (championship is null) return null;

        var created = await userInChampionshipRepository.AddAsync(dto.ToEntity(user, championship));
        return created.ToDto();
    }

    /// <summary>
    /// Обновление данных существующей записи пользователя
    /// </summary>
    public async Task<bool> UpdateAsync(int id, UserInChampionshipEditDto dto)
    {
        var usersChampionship = await usersChampionshipRepository.GetByIdAsync(dto.ChampionshipId);
        if (usersChampionship == null) return false;
        
        var user = await userRepository.GetByIdAsync(dto.UserId);
        if (user == null) return false;

        var userInChampionship = dto.ToEntity(user, usersChampionship);
        userInChampionship.Id = id;

        return await userInChampionshipRepository.UpdateAsync(userInChampionship);
    }

    /// <summary>
    /// Удаление записи по ее Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var participation = await userInChampionshipRepository.GetByIdAsync(id);
        if (participation is null) return false;
        if (participation.UserId != userId) return false;

        return await userInChampionshipRepository.DeleteAsync(id);
    }
}