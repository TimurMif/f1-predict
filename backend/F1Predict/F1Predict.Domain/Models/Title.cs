namespace F1Predict.Domain.Models;

/// <summary>
/// Класс для титулов пользователя в чемпионате прогнозов
/// </summary>
public class Title
{
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }
    public required User User { get; set; }
    /// <summary>
    /// Год, когда получен титул
    /// </summary>
    public required int Year { get; set; }
}