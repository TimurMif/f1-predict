namespace F1Predict.Contracts.Dto;

/// <summary>
/// Хотя бы один из VkId / TelegramId должен быть заполнен (проверка в сервисе).
/// </summary>
public record UserEditDto(
    string FirstName,
    string? LastName,
    string? VkId,
    string? TelegramId);