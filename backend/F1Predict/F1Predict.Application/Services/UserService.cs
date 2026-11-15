using F1Predict.Application.Interfaces;
using F1Predict.Contracts.Dto;
using F1Predict.Contracts.ServiceInterfaces;
using F1Predict.Domain.Models;
using F1Predict.Application.Mapping;

namespace F1Predict.Application.Services;

/// <summary>
/// Сервис для работы с пользователями
/// </summary>
public class UserService(IRepository<User> userRepository) : IUserService
{
    /// <summary>
    /// Возвращает список всех пользователей
    /// </summary>
    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.Select(u=>u.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает пользователя по его Id
    /// </summary>
    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user?.ToDto();
    }

    /// <summary>
    /// Создает нового пользователя
    /// </summary>
    public async Task<UserDto> CreateAsync(UserEditDto dto)
    {
        await EnsureAuthIdsUnique(dto.VkId, dto.TelegramId, excludeId: null);
        var created = await userRepository.AddAsync(dto.ToEntity());
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные о существующем пользователей по его Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, int userId, UserEditDto dto)
    {
        if (id != userId) return false;
        var user = await userRepository.GetByIdAsync(id);
        if (user is null) return false;

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        return await userRepository.UpdateAsync(user);
    }

    /// <summary>
    /// Удаляет пользователя по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id, int userId)
    {
        if (id != userId) return false;
        return await userRepository.DeleteAsync(id);
    }
    
    private async Task EnsureAuthIdsUnique(string? vkId, string? telegramId, int? excludeId)
    {
        var all = await userRepository.GetAllAsync();
        if (!string.IsNullOrWhiteSpace(vkId) && all.Any(u => u.VkId == vkId && u.Id != excludeId))
            throw new InvalidOperationException("Этот VK уже привязан к другому аккаунту");
        if (!string.IsNullOrWhiteSpace(telegramId) && all.Any(u => u.TelegramId == telegramId && u.Id != excludeId))
            throw new InvalidOperationException("Этот Telegram уже привязан к другому аккаунту");
    }
}