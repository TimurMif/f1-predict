namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования Чемпионата пользователей
/// </summary>
public record UsersChampionshipEditDto
{
    /// <summary>
    /// Год, когда проходит чемпионат
    /// </summary>
    public int Year { get; init; }
}