namespace F1Predict.Application.Exceptions;

/// <summary>
/// Некорректные данные запроса, которые нельзя проверить атрибутами DTO (HTTP 400).
/// </summary>
public class BadRequestException(string message) : Exception(message);