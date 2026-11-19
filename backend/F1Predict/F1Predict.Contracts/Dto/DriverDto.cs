namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс для Dto Пилота
/// </summary>
public record DriverDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; init; }
    
    /// <summary>
    /// Номер
    /// </summary>
    public required string Number { get; init; }
    
    /// <summary>
    /// Национальность
    /// </summary>
    public required string Nationality { get; init; }
    
    /// <summary>
    /// Очки в сезоне
    /// </summary>
    public int Points { get; init; }
    
    /// <summary>
    /// Навигационное свойство команды
    /// </summary>
    public required ConstructorDto Constructor { get; init; }
}