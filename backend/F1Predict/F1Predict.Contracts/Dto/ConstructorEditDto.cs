using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования команды
/// </summary>
public record ConstructorEditDto
{
    /// <summary>
    /// Название команды
    /// </summary>
    [Required(ErrorMessage = "Название команды обязательно")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название: от 2 до 100 символов")]
    public required string Name { get; init; }

    /// <summary>
    /// Национальность
    /// </summary>
    [Required(ErrorMessage = "Национальность обязательна")]
    [StringLength(60, MinimumLength = 2)]
    public required string Nationality { get; init; }

    /// <summary>
    /// Очки в сезоне
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Очки не могут быть отрицательными")]
    public int Points { get; init; }
}