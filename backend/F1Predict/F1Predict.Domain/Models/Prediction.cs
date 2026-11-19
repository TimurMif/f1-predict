namespace F1Predict.Domain.Models;

/// <summary>
/// Класс, который описывает прогноз на сессию F1
/// </summary>
public class Prediction
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пользователя
    /// </summary>
    public required User User { get; set; }
    
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public int SessionId { get; set; }
    
    /// <summary>
    /// Навигационное свойство сессии
    /// </summary>
    public required Session Session { get; set; }
    
    /// <summary>
    /// Идентификатор пилота, которые будет первым
    /// </summary>
    public int DriverTopOneId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пилота, который будет первым
    /// </summary>
    public required Driver DriverTopOne { get; set; }
    
    /// <summary>
    /// Идентификатор пилота, который будет вторым
    /// </summary>
    public int DriverTopTwoId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пилота, который будет вторым
    /// </summary>
    public required Driver DriverTopTwo { get; set; }
    
    /// <summary>
    /// Идентификатор пилота, которые будет третьим
    /// </summary>
    public int DriverTopThreeId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пилота, который будет третьим
    /// </summary>
    public required Driver DriverTopThree { get; set; }
    
    /// <summary>
    /// Лучшее время сессии
    /// </summary>
    public int? BestSessionLapTimeMs { get; set; }
    
    /// <summary>
    /// Подсчитанное количество очков за этот прогноз
    /// </summary>
    public int TotalPointsForThisPrediction { get; set; }
    
    /// <summary>
    /// Когда был создан прогноз
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Когда он был последний раз отредактирован
    /// </summary>
    public DateTime? Edited { get; set; }
}