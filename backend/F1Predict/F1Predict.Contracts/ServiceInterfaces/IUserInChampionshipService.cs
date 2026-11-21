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
    /// Исключение участника из чемпионата (только создатель; создателя исключить нельзя)
    /// </summary>
    public Task<bool> KickAsync(int championshipId, int targetUserId, int currentUserId);
    
    /// <summary>
    /// Выход из чемпионата (создатель выйти не может)
    /// </summary>
    public Task<bool> LeaveAsync(int championshipId, int userId);
}