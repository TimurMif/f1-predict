using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

public record GrandPrixEditDto
{
    [Required(ErrorMessage = "Название Гран-при обязательно")]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Название трассы обязательно")]
    [StringLength(100, MinimumLength = 2)]
    public required string CircuitName { get; init; }

    [Required(ErrorMessage = "Страна обязательна")]
    [StringLength(60, MinimumLength = 2)]
    public required string Country { get; init; }

    [Range(1, 30, ErrorMessage = "Номер этапа: от 1 до 30")]
    public required int RoundNumber { get; init; }

    [Range(1950, 2100, ErrorMessage = "Некорректный год сезона")]
    public required int Year { get; init; }
}