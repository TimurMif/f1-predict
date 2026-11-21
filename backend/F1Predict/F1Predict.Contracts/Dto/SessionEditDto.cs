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
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название сессии: от 2 до 100 символов")]
    public required string Name { get; init; }
    
    /// <summary>
    /// Тип сессии Type: Qualifying, Race, Sprint, SprintQualifying, FreePractice.
    /// </summary>
    [EnumDataType(typeof(SessionType), ErrorMessage = "Недопустимый тип сессии")]
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
    [EnumDataType(typeof(SessionStatus), ErrorMessage = "Недопустимый статус сессии")]
    public required SessionStatus Status { get; init; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateEnd <= DateStart)
            yield return new ValidationResult(
                "Окончание сессии должно быть позже начала", [nameof(DateEnd)]);
    }
}