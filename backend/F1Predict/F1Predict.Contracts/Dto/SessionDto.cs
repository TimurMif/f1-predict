using F1Predict.Domain.Models;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Сессии
/// </summary>
public record SessionDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Идентификатор Гран при
    /// </summary>
    public int GrandPrixId { get; init; }
    
    /// <summary>
    /// Название сессии
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Тип сессии Type: Qualifying, Race, Sprint, SprintQualifying, FreePractice.
    /// </summary>
    public required SessionType Type { get; init; }
    
    /// <summary>
    /// Дата и время начала сессии
    /// </summary>
    public DateTime DateStart { get; init; }
    
    /// <summary>
    /// Дата и время окончания сессии
    /// </summary>
    public DateTime DateEnd { get; init; }
    
    /// <summary>
    /// Статус сессии Status: Scheduled, InProgress, Completed, Cancelled.
    /// </summary>
    public required SessionStatus Status { get; init; }
}