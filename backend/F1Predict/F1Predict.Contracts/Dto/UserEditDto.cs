namespace F1Predict.Contracts.Dto;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Класс Dto для редактирования Пользователей
/// </summary>
public record UserEditDto
{
    /// <summary>
    /// Имя
    /// </summary>
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя: от 2 до 50 символов")]
    public required string FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия: от 2 до 50 символов")]
    public string? LastName { get; init; }

    /// <summary>
    /// Id во ВКонтакте. Задаётся при регистрации, при изменении профиля игнорируется.
    /// </summary>
    public string? VkId { get; init; }

    /// <summary>
    /// Id в Telegram. Задаётся при регистрации, при изменении профиля игнорируется.
    /// </summary>
    public string? TelegramId { get; init; }
}