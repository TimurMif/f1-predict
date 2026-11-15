using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

public record DriverResultEditDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный SessionId")]
    public int SessionId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverId")]
    public int DriverId { get; init; }

    [Range(1, 22, ErrorMessage = "Позиция должна быть от 1 до 22")]
    public int Position { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Время круга должно быть положительным")]
    public int? BestLapTimeMs { get; init; }
}