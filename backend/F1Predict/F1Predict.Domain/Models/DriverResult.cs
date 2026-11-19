namespace F1Predict.Domain.Models;

/// <summary>
/// Класс, который описывает результат конкретного пилота в конкретной сессии
/// </summary>
public class DriverResult
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public int SessionId { get; set; }
    
    /// <summary>
    /// Навигационное свойство сессии
    /// </summary>
    public required Session Session { get; set; }
    
    /// <summary>
    /// Идентификатор пилота
    /// </summary>
    public int DriverId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пилота
    /// </summary>
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