namespace F1Predict.Domain.Models;


/// <summary>
/// Класс для команды формулы.
/// </summary>
public class Constructor
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название команды
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Национальность
    /// </summary>
    public required string Nationality { get; set; }
    
    /// <summary>
    /// Заработанные очки
    /// </summary>
    public int Points { get; set; }
}