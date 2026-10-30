namespace F1Predict.Domain.Models;

/// <summary>
/// Класс для титулов в чемпионате прогнозов
/// </summary>
public class Title
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public required int Year { get; set; }
}