namespace F1Predict.Domain.Models;

/// <summary>
/// Класс, описывающий пользовательский чемпионат
/// </summary>
public class UsersChampionship
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Год, когда проводится чемпионат
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Пользователь-хост который создал чемпионат для других пользователей
    /// </summary>
    public int CreatedByUserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство пользователя-создателя
    /// </summary>
    public required User CreatedByUser { get; set; }
   
    /// <summary>
    /// Обязательный 6-значный пригласительный код
    /// </summary>
    public required string InviteCode { get; set; }
    
    /// <summary>
    /// Пользователи, которые учатвуют в этом чемпионате
    /// </summary>
    public ICollection<UserInChampionship> Users { get; set; } = new List<UserInChampionship>();
    
}