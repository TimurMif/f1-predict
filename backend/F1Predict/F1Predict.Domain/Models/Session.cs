namespace F1Predict.Domain.Models;

/// <summary>
/// Класс, который описывает любую сессию этапа F1
/// </summary>
public class Session
{
    public int Id { get; set; }
    /// <summary>
    /// Название сессии
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Идентификатор Гран При
    /// </summary>
    public int GrandPrixId { get; set; }
    public required GrandPrix GrandPrix { get; set; }
    /// <summary>
    /// Тип сессии
    /// </summary>
    public SessionType Type { get; set; }
    /// <summary>
    /// Начало сессии
    /// </summary>
    public DateTime DateStart { get; set; }
    /// <summary>
    /// Конец сессии
    /// </summary>
    public DateTime DateEnd { get; set; }
    /// <summary>
    /// Статус сессии: запланирована, прошла, отменена, сейчас идет
    /// </summary>
    public required SessionStatus Status { get; set; }
    /// <summary>
    /// Результаты сессии
    /// </summary>
    public ICollection<DriverResult> Results { get; set; }= new List<DriverResult>();
}