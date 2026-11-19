using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Гран при
/// </summary>
public record GrandPrixEditDto
{
    /// <summary>
    /// Название
    /// </summary>
    [Required(ErrorMessage = "Название Гран-при обязательно")]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; init; }

    /// <summary>
    /// Название трассы
    /// </summary>
    [Required(ErrorMessage = "Название трассы обязательно")]
    [StringLength(100, MinimumLength = 2)]
    public required string CircuitName { get; init; }

    /// <summary>
    /// Страна
    /// </summary>
    [Required(ErrorMessage = "Страна обязательна")]
    [StringLength(60, MinimumLength = 2)]
    public required string Country { get; init; }

    /// <summary>
    /// Порядковый номер этапа в сезоне
    /// </summary>
    [Range(1, 30, ErrorMessage = "Номер этапа: от 1 до 30")]
    public required int RoundNumber { get; init; }

    /// <summary>
    /// Год
    /// </summary>
    [Range(1950, 2100, ErrorMessage = "Некорректный год сезона")]
    public required int Year { get; init; }
}