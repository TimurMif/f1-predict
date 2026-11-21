using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса для чемпионатов пользователей
/// </summary>
public interface IUsersChampionshipService
{
    /// <summary>
    /// Возвращает чемпионаты, в которых пользователь участвует или которые создал
    /// </summary>
    public Task<List<UsersChampionshipDto>> GetAllAsync(int currentUserId);

    /// <summary>
    /// Возвращает чемпионат по Id (доступен только участникам и создателю)
    /// </summary>
    public Task<UsersChampionshipDto?> GetByIdAsync(int id, int currentUserId);

    /// <summary>
    /// Возвращает таблицу участников чемпионата по убыванию очков
    /// </summary>
    public Task<List<UserInChampionshipDto>> GetStandingsAsync(int id, int currentUserId);

    /// <summary>
    /// Создает новый чемпионат; создатель сразу становится участником
    /// </summary>
    public Task<UsersChampionshipDto> CreateAsync(int userId, UsersChampionshipEditDto dto);

    /// <summary>
    /// Обновляет данные чемпионата (только создатель)
    /// </summary>
    public Task<bool> UpdateAsync(int id, int userId, UsersChampionshipEditDto dto);

    /// <summary>
    /// Возвращает код приглашения (только создатель)
    /// </summary>
    public Task<string?> GetInviteCodeAsync(int id, int userId);

    /// <summary>
    /// Удаляет чемпионат вместе с записями участников (только создатель)
    /// </summary>
    public Task<bool> DeleteAsync(int id, int userId);
}