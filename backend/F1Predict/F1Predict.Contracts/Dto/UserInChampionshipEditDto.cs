namespace F1Predict.Contracts.Dto;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Класс Dto для редактирования Записи пользователя в чемпионате
/// </summary>
public record UserInChampionshipEditDto
{
    /// <summary>
    /// Код приглашения
    /// </summary>
    [Required(ErrorMessage = "Код приглашения обязателен")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Код приглашения — ровно 6 символов")]
    public required string InviteCode { get; init; }
}