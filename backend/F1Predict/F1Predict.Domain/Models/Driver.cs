namespace F1Predict.Domain.Models;

/// <summary>
/// Класс для пилота формулы. Он нужен для отображения статистики конкретного пилота. Его национальность, его текущие очки в реальном чемпионате
/// </summary>
public class Driver
{
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
    /// Класс объекта команды
    /// </summary>
    public required Constructor Constructor { get; set; }
}