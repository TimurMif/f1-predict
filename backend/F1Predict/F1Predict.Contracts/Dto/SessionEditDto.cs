using F1Predict.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace F1Predict.Contracts.Dto;

public record SessionEditDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный GrandPrixId")]
    public int GrandPrixId { get; init; }
    
    [Required(ErrorMessage = "Название сессии обязательно")]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; init; }
    public required SessionType Type { get; init; }
    public DateTime DateStart { get; init; }
    public DateTime DateEnd { get; init; }
    public required SessionStatus Status { get; init; }
}