namespace F1Predict.Domain.Models;

/// <summary>
/// Класс, который описывает результат конкретного пилота в конкретной сессии
/// </summary>
public class SessionResult
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public required Session Session { get; set; }
    public int DriverId { get; set; }
    public required Driver Driver { get; set; }

    /// <summary>
    /// Итоговая позиция в сессии (1 = победитель или поул)
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// Лучшее время круга пилота в сессии, в миллисекундах
    /// </summary>
    public int? BestLapTimeMs { get; set; }
}