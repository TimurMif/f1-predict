namespace F1Predict.Domain.Models;


/// <summary>
/// Класс для команды формулы. Он нужен для отображения статистики конкретной команды. Ее национальность, ее текущие очки в реальном чемпионате
/// </summary>
public class Constructor
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Nationality { get; set; }
    public int Points { get; set; }
}