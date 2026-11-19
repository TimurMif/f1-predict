namespace F1Predict.Domain.Models;

/// <summary>
/// Класс-профиль пользователя внутри чемпионата.
/// Здесь отдельный счетчик очков, который существует только внутри чемпионата.
/// </summary>
public class UserInChampionship
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор чемпионата
    /// </summary>
    public int ChampionshipId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пользовательского чемпионата
    /// </summary>
    public required UsersChampionship Championship { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пользователя
    /// </summary>
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