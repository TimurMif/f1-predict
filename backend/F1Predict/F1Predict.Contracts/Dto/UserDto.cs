namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Пользователя
/// </summary>
public record UserDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Ник
    /// </summary>
    public required string Nickname { get; init; }
    
    /// <summary>
    /// Очки
    /// </summary>
    public required int Points { get; init; }
    
    /// <summary>
    /// Навигационное свойство списка полученных титулов
    /// </summary>
    public required List<TitleDto> Titles { get; init; }
    
    /// <summary>
    /// Идентификатор чемпионата, где участвует пользователь
    /// </summary>
    public int? ChampionshipId { get; init; }
}