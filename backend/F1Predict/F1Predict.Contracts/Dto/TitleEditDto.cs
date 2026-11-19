using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Титулов
/// </summary>
public record TitleEditDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный UserId")]
    public required int UserId { get; init; }

    /// <summary>
    /// Год, когда был получен титул
    /// </summary>
    [Range(1950, 2200, ErrorMessage = "Некорректный год сезона")]
    public required int Year { get; init; }
}