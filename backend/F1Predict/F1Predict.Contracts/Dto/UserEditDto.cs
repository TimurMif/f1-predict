namespace F1Predict.Contracts.Dto;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Хотя бы один из VkId / TelegramId должен быть заполнен (проверка в сервисе).
/// </summary>
public record UserEditDto
{
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, MinimumLength = 2)]
    public required string FirstName { get; init; }
    
    [StringLength(50, MinimumLength = 2)]
    public required string LastName { get; init; }
    public required string? VkId { get; init; }
    public required string? TelegramId { get; init; }
}