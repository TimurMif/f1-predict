namespace F1Predict.Contracts.Dto;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Класс Dto для редактирования Пользователей
/// </summary>
public record UserEditDto
{
    /// <summary>
    /// Ник
    /// </summary>
    [Required(ErrorMessage = "Ник обязателен")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Ник: от 2 до 50 символов")]
    public required string Nickname { get; init; }

    /// <summary>
    /// Id во ВКонтакте. Задаётся при регистрации, при изменении профиля игнорируется.
    /// </summary>
    public string? VkId { get; init; }

    /// <summary>
    /// Id в Telegram. Задаётся при регистрации, при изменении профиля игнорируется.
    /// </summary>
    public string? TelegramId { get; init; }
}