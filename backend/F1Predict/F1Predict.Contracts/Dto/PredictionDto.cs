namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Прогноза
/// </summary>
public record PredictionDto
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
    /// Идентификатор сессии
    /// </summary>
    public int SessionId { get; init; }
    
    /// <summary>
    /// Идентификатор пилота, который прогнозируемо будет на первом месте
    /// </summary>
    public int DriverTopOneId { get; init; }
    
    /// <summary>
    /// Идентификатор пилота, который прогнозируемо будет на втором месте
    /// </summary>
    public int DriverTopTwoId { get; init; }
    
    /// <summary>
    /// Идентификатор пилота, который прогнозируемо будет на третьем месте
    /// </summary>
    public int DriverTopThreeId { get; init; }
    
    /// <summary>
    /// Лучший круг в сессии
    /// </summary>
    public int? BestSessionLapTimeMs { get; init; }
    
    /// <summary>
    /// Начисленные очки за прогноз
    /// </summary>
    public int TotalPointsForThisPrediction { get; init; }
    
    /// <summary>
    /// Дата создания прогноза
    /// </summary>
    public DateTime Created { get; init; }
    
    /// <summary>
    /// Дата последнего изменения прогноза
    /// </summary>
    public DateTime? Edited { get; init; }
}