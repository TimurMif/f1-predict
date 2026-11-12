using F1Predict.Domain.Models;

namespace F1Predict.Contracts.Dto;

public record SessionEditDto
{
    public int GrandPrixId { get; init; }
    public required string Name { get; init; }
    public required SessionType Type { get; init; }
    public DateTime DateStart { get; init; }
    public DateTime DateEnd { get; init; }
    public required SessionStatus Status { get; init; }
}