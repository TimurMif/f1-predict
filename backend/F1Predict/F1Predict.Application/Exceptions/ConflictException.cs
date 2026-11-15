namespace F1Predict.Application.Exceptions;

/// <summary>Нарушено бизнес-правило или конфликт состояния (HTTP 409).</summary>
public class ConflictException(string message) : Exception(message);