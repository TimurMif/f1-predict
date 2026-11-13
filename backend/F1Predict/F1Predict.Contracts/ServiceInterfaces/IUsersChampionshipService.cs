using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для чемпионатов пользователей
/// </summary>
public interface IUsersChampionshipService
{
    /// <summary>
    /// Возвращает список всех чемпионатов пользователей
    /// </summary>
    public Task<List<UsersChampionshipDto>> GetAllAsync();

    /// <summary>
    /// Возвращает чемпионат пользователей по его Id
    /// </summary>
    public Task<UsersChampionshipDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новый чемпионат пользователей
    /// </summary>
    public Task<UsersChampionshipDto> CreateAsync(UsersChampionshipEditDto dto);

    /// <summary>
    /// Обновляет данные существующего чемпионата пользователей
    /// </summary>
    public Task<bool> UpdateAsync(int id, UsersChampionshipEditDto dto);

    /// <summary>
    /// Удаляет чемпионат пользователей по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}