namespace F1Predict.Domain.Models;

/// <summary>
/// Статус сессии
/// </summary>
public enum SessionStatus
{
    /// <summary>
    /// Запланировано
    /// </summary>
    Scheduled,
    
    /// <summary>
    /// В процессе
    /// </summary>
    InProgress,
    
    /// <summary>
    /// Завершена
    /// </summary>
    Completed,
    
    /// <summary>
    /// Отменена
    /// </summary>
    Cancelled
}