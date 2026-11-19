using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Результата пилота в сессии.
/// BestLapTimeMs — время круга в миллисекундах (84758 = 1:24.758).
/// </summary>
public record DriverResultEditDto
{
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный SessionId")]
    public int SessionId { get; init; }
    
    /// <summary>
    /// Идентификатор пилота
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverId")]
    public int DriverId { get; init; }

    /// <summary>
    /// Позиция в сессии
    /// </summary>
    [Range(1, 22, ErrorMessage = "Позиция должна быть от 1 до 22")]
    public int Position { get; init; }

    /// <summary>
    /// Лучший круг пилота в сессии
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Время круга должно быть положительным")]
    public int? BestLapTimeMs { get; init; }
}