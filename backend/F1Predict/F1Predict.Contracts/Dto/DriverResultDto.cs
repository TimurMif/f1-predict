namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Результата пилота в сессии.
/// BestLapTimeMs — время круга в миллисекундах (84758 = 1:24.758).
/// </summary>
public record DriverResultDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public int SessionId {get; init; }
    
    /// <summary>
    /// Навигационное свойство пилота
    /// </summary>
    public required DriverDto Driver { get; init; }
    
    /// <summary>
    /// Позиция в сессии
    /// </summary>
    public int Position { get; init; }
    
    /// <summary>
    /// Лучший круг пилота
    /// </summary>
    public int? BestLapTimeMs { get; init; }
}