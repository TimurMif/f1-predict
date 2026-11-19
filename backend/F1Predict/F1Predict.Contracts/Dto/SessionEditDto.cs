using F1Predict.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Сессии
/// </summary>
public record SessionEditDto
{
    /// <summary>
    /// Идентификатор Гран при
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный GrandPrixId")]
    public int GrandPrixId { get; init; }
    
    /// <summary>
    /// Название сессии
    /// </summary>
    [Required(ErrorMessage = "Название сессии обязательно")]
    [StringLength(100, MinimumLength = 2)]
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