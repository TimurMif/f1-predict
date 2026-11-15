namespace F1Predict.Application.Exceptions;

/// <summary>Запрошенная сущность не найдена (HTTP 404).</summary>
public class NotFoundException(string message) : Exception(message);