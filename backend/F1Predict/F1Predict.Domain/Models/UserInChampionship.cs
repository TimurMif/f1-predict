namespace F1Predict.Domain.Models;

/// <summary>
/// Класс-профиль пользователя внутри чемпионата.
/// Здесь отдельный счетчик очков, который существует только внутри чемпионата.
/// </summary>
public class UserInChampionship
{
    public int Id { get; set; }
    public int ChampionshipId { get; set; }
    public required UsersChampionship Championship { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    /// <summary>
    /// Очки набранные игроком за его прогнозы, пока он находился в чемпионате
    /// </summary>
    public int TotalPoints { get; set; }
    public required DateTime JoinedAt { get; set; }
}