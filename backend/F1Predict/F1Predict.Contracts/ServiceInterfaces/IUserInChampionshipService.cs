using F1Predict.Contracts.Dto;

namespace F1Predict.Contracts.ServiceInterfaces;

/// <summary>
/// Интерфейс сервиса участия пользователей в чемпионатах
/// </summary>
public interface IUserInChampionshipService
{
    /// <summary>
    /// Вступление в чемпионат по пригласительному коду
    /// </summary>
    public Task<UserInChampionshipDto> JoinAsync(int userId, UserInChampionshipEditDto dto);

    /// <summary>
    /// Выход из чемпионата (создатель выйти не может)
    /// </summary>
    public Task<bool> LeaveAsync(int championshipId, int userId);
}