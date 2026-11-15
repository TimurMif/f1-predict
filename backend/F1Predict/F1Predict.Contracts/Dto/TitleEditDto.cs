using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

public record TitleEditDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный UserId")]
    public required int UserId { get; init; }

    [Range(1950, 2200, ErrorMessage = "Некорректный год сезона")]
    public required int Year { get; init; }
}