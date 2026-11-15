namespace F1Predict.Domain.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    /// <summary>
    /// Фамилия для пользователя не обязательна, но при желании он может добавить
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Id для авторизаций по Vk и Telegram
    /// </summary>
    public string? VkId { get; set; }
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
    public UserInChampionship? UserInChampionship { get; set; }
}