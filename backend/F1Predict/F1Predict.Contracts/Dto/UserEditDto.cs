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
    [StringLength(50, MinimumLength = 2)]
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    [StringLength(50, MinimumLength = 2)]
    public required string? LastName { get; init; }
    
    /// <summary>
    /// Идентификатор через Vk
    /// </summary>
    public required string? VkId { get; init; }
    
    /// <summary>
    /// Идентификатор через Telegram
    /// </summary>
    public required string? TelegramId { get; init; }
}