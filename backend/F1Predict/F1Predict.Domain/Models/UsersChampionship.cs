namespace F1Predict.Domain.Models;

public class UsersChampionship
{
    public int Id { get; set; }
    public int Year { get; set; }
    /// <summary>
    /// Пользователь-хост который создал чемпионат для других пользователей
    /// и может с помощью invite code приглашать других
    /// </summary>
    public int CreatedByUserId { get; set; }
    public required User CreatedByUser { get; set; }
    /// <summary>
    /// Обязательный 6-значный пригласительный код, которым Users делятся друг с другом чтобы пригласить в чемпионат
    /// </summary>
    public required string InviteCode { get; set; }
    /// <summary>
    /// Пользователи, которые соревнуются друг с другом.
    /// </summary>
    public ICollection<UserInChampionship> Users { get; set; } = new List<UserInChampionship>();
    
}