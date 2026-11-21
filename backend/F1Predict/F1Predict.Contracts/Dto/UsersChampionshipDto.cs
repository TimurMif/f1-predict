using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Чемпионата пользователей
/// </summary>
public record UsersChampionshipDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Год, когда проходит чемпионат
    /// </summary>
    public int Year { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя, который создал чемпионат
    /// </summary>
    public int CreatedByUserId { get; init; }
    
    /// <summary>
    /// Навигационное свойство списка записей всех пользователей, которые участвуют в чемпионате
    /// </summary>
    public required List<UserInChampionshipDto> Users { get; init; }
}