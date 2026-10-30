namespace F1Predict.Domain.Models;

/// <summary>
/// Класс для пилота формулы. Он нужен для отображения статистики конкретного пилота. Его национальность, его текущие очки в реальном чемпионате
/// </summary>
public class Driver
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Number { get; set; }
    public required string Nationality { get; set; }
    public int Points { get; set; }
    public int ConstructorId { get; set; }
    public required Constructor Constructor { get; set; }
}