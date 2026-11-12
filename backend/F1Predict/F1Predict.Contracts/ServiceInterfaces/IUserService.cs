using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для пользователей
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Возвращает список всех пользователей
    /// </summary>
    public Task<List<UserDto>> GetAllAsync();

    /// <summary>
    /// Возвращает пользователя по его Id
    /// </summary>
    public Task<UserDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового пользователя
    /// </summary>
    public Task<UserDto> CreateAsync(UserEditDto dto);

    /// <summary>
    /// Обновляет данные существующего пользователя
    /// </summary>
    public Task<bool> UpdateAsync(int id, UserEditDto dto);

    /// <summary>
    /// Удаляет пользователя по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}
