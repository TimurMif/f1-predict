using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Прогноза
/// </summary>
public record PredictionEditDto
{
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный SessionId")]
    public int SessionId { get; init; }

    /// <summary>
    /// Идентификатор пилота, который прогнозируемо будет на первом месте
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverTopOneId")]
    public int DriverTopOneId { get; init; }

    /// <summary>
    /// Идентификатор пилота, который прогнозируемо будет на втором месте
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverTopTwoId")]
    public int DriverTopTwoId { get; init; }

    /// <summary>
    /// Идентификатор пилота, который прогнозируемо будет на третьем месте
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverTopThreeId")]
    public int DriverTopThreeId { get; init; }

    /// <summary>
    /// Лучший круг в сессии
    /// </summary>
    [Range(1, 600_000, ErrorMessage = "Время круга: от 1 мс до 10 минут")]
    public int? BestSessionLapMs { get; init; }
}