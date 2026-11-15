using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для записей пользователей в чемпионатах
/// </summary>
public interface IUserInChampionshipService
{
    /// <summary>
    /// Возвращает список всех записей пользователя в чемпионате
    /// </summary>
    public Task<List<UserInChampionshipDto>> GetAllAsync();

    /// <summary>
    /// Возвращает запись пользователя в чемпионате по его Id
    /// </summary>
    public Task<UserInChampionshipDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новую запись пользователя в чемпионате
    /// </summary>
    Task<UserInChampionshipDto?> CreateAsync(int userId, UserInChampionshipEditDto dto);

    /// <summary>
    /// Удаляет запись пользователя в чемпионате по его Id
    /// </summary>
    Task<bool> DeleteAsync(int id, int userId);
}