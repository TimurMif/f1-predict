namespace F1Predict.Domain.Models;

/// <summary>
/// Класс для пользователя
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Id для авторизаций по Vk
    /// </summary>
    public string? VkId { get; set; }
    
    /// <summary>
    /// Id для авторизаций по Telegram
    /// </summary>
    public string? TelegramId { get; set; }
    
    /// <summary>
    /// Все прогнозы пользователя, изначально их нету.
    /// </summary>
    public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    
    /// <summary>
    /// Очки за сезон
    /// </summary>
    public int Points { get; set; }
    
    /// <summary>
    /// Титулы чемпиона прогнозов у игрока, изначально их 0.
    /// </summary>
    public ICollection<Title> Titles { get; set; }= new List<Title>();
    
    /// <summary>
    /// Навигационное свойство чемпионата, в котором состоит пользователь
    /// </summary>
    public UserInChampionship? UserInChampionship { get; set; }
}