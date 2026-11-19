using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Класс Dto для редактирования пилота
/// </summary>
public record DriverEditDto
{
    /// <summary>
    /// Имя
    /// </summary>
    [Required(ErrorMessage = "Имя пилота обязательно")]
    [StringLength(50, MinimumLength = 2)]
    public required string FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Required(ErrorMessage = "Фамилия пилота обязательна")]
    [StringLength(50, MinimumLength = 2)]
    public required string LastName { get; init; }

    /// <summary>
    /// Номер
    /// </summary>
    [Required(ErrorMessage = "Номер обязателен")]
    [RegularExpression("^[1-9][0-9]?$", ErrorMessage = "Номер пилота: от 1 до 99")]
    public required string Number { get; init; }

    /// <summary>
    /// Национальность
    /// </summary>
    [Required(ErrorMessage = "Национальность обязательна")]
    [StringLength(60, MinimumLength = 2)]
    public required string Nationality { get; init; }

    /// <summary>
    /// Очки за сезон
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Очки не могут быть отрицательными")]
    public int Points { get; init; }

    /// <summary>
    /// Идентификатор команды
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ConstructorId")]
    public int ConstructorId { get; init; }
}