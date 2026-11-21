namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Записи пользователя в чемпионате
/// </summary>
public record UserInChampionshipDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Идентификатор чемпионата
    /// </summary>
    public int ChampionshipId { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; init; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string UserName { get; init; }
    
    /// <summary>
    /// Очки в чемпионате
    /// </summary>
    public int TotalPoints { get; init; }
    
    /// <summary>
    /// Дата и время вступления в чемпионат
    /// </summary>
    public DateTime JoinedAt { get; init; }
}