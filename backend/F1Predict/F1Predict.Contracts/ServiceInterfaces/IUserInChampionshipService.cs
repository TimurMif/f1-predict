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
    public Task<UserInChampionshipDto> CreateAsync(UserInChampionshipEditDto dto);

    /// <summary>
    /// Обновляет данные существующей записи пользователя в чемпионате
    /// </summary>
    public Task<bool> UpdateAsync(int id, UserInChampionshipEditDto dto);

    /// <summary>
    /// Удаляет запись пользователя в чемпионате по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}