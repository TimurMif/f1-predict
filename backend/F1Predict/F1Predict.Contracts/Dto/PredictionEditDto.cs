using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

public record PredictionEditDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный SessionId")]
    public int SessionId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverTopOneId")]
    public int DriverTopOneId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverTopTwoId")]
    public int DriverTopTwoId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный DriverTopThreeId")]
    public int DriverTopThreeId { get; init; }

    [Range(1, 600_000, ErrorMessage = "Время круга: от 1 мс до 10 минут")]
    public int? BestSessionLapMs { get; init; }
}