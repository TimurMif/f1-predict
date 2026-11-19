namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto команды Формулы 1
/// </summary>
public record ConstructorDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Национальность
    /// </summary>
    public required string Nationality { get; init; }
    
    /// <summary>
    /// Очки в сезоне
    /// </summary>
    public required int Points { get; init; }
}