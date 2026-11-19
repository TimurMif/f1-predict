namespace F1Predict.Domain.Models;

/// <summary>
/// Класс для пилота формулы.
/// </summary>
public class Driver
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
    public required string LastName { get; set; }
    
    /// <summary>
    /// Номер
    /// </summary>
    public required string Number { get; set; }
    
    /// <summary>
    /// Национальность
    /// </summary>
    public required string Nationality { get; set; }
    
    /// <summary>
    /// Заработанные очки
    /// </summary>
    public int Points { get; set; }
    
    /// <summary>
    /// Идентификатор команды
    /// </summary>
    public int ConstructorId { get; set; }
    
    /// <summary>
    /// Навигационное свойство команды
    /// </summary>
    public required Constructor Constructor { get; set; }
}