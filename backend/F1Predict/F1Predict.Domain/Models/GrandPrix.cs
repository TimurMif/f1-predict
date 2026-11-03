namespace F1Predict.Domain.Models;

/// <summary>
/// Класс, который описывает один этап F1
/// </summary>
public class GrandPrix
{
    public int Id { get; set; }
    /// <summary>
    /// Название Гран При
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Название трассы
    /// </summary>
    public required string CircuitName { get; set; }
    /// <summary>
    /// Название страны
    /// </summary>
    public required string Country { get; set; }
    /// <summary>
    /// Порядковый номер этапа
    /// </summary>
    public required int RoundNumber { get; set; }
    /// <summary>
    /// Год, когда проводится Гран При
    /// </summary>
    public required int Year { get; set; }
    /// <summary>
    /// Коллекция сессий, которые входят в конкретный Гран При
    /// </summary>
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}