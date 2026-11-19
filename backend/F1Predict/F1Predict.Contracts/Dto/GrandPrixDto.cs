namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Гран при
/// </summary>
public record GrandPrixDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Название Гран при
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Название трассы
    /// </summary>
    public required string CircuitName { get; init; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public required string Country { get; init; }
    
    /// <summary>
    /// Порядковый номер этапа в сезоне
    /// </summary>
    public required int RoundNumber { get; init; }
    
    /// <summary>
    /// Год
    /// </summary>
    public required int Year { get; init; }
    
    /// <summary>
    /// Навигационное свойство со списком сессий, которые включены в это Гран при
    /// </summary>
    public required List<SessionDto> Sessions { get; init; }
}