using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Чемпионата пользователей
/// </summary>
public record UsersChampionshipEditDto
{
    /// <summary>
    /// Год, когда проходит чемпионат
    /// </summary>
    [Range(2020, 2100, ErrorMessage = "Некорректный год сезона")]
    public int Year { get; init; }
}