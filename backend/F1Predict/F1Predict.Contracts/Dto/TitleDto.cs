namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Титула
/// </summary>
public record TitleDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; init; }
    
    /// <summary>
    /// Год, когда был получен титул
    /// </summary>
    public required int Year { get; init; }
}