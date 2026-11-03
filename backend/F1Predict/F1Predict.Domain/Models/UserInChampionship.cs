namespace F1Predict.Domain.Models;

/// <summary>
/// Класс-профиль пользователя внутри чемпионата.
/// Здесь отдельный счетчик очков, который существует только внутри чемпионата.
/// </summary>
public class UserInChampionship
{
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор чемпионата
    /// </summary>
    public int ChampionshipId { get; set; }
    public required UsersChampionship Championship { get; set; }
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }
    public required User User { get; set; }
    /// <summary>
    /// Очки набранные игроком за его прогнозы, пока он находился в чемпионате
    /// </summary>
    public int TotalPoints { get; set; }
    /// <summary>
    /// Когда пользователь присоединился к чемпионату
    /// </summary>
    public required DateTime JoinedAt { get; set; }
}