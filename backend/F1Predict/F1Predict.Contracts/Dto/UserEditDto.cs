namespace F1Predict.Contracts.Dto;

/// <summary>
/// Хотя бы один из VkId / TelegramId должен быть заполнен (проверка в сервисе).
/// </summary>
public record UserEditDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string? VkId { get; init; }
    public required string TelegramId { get; init; }
}