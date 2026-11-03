namespace F1Predict.Domain.Models;


/// <summary>
/// Класс для команды формулы. Он нужен для отображения статистики конкретной команды. Ее национальность, ее текущие очки в реальном чемпионате
/// </summary>
public class Constructor
{
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