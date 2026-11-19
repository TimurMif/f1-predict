namespace F1Predict.Domain.Models;

/// <summary>
/// Тип сессии
/// </summary>
public enum SessionType
{
    /// <summary>
    /// Квалификация
    /// </summary>
    Qualifying,
    
    /// <summary>
    /// Гонка
    /// </summary>
    Race,
    
    /// <summary>
    /// Спринт
    /// </summary>
    Sprint,
    
    /// <summary>
    /// Спринт квалификация
    /// </summary>
    SprintQualifying,
}