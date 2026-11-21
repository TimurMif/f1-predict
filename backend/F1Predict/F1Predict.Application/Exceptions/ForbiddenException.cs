namespace F1Predict.Application.Exceptions;

/// <summary>
/// Действие запрещено текущему пользователю (HTTP 403).
/// </summary>
public class ForbiddenException(string message) : Exception(message);