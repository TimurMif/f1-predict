namespace F1Predict.Contracts.Dto;

public record GrandPrixEditDto
{
    public required string Name { get; init; }
    public required string CircuitName { get; init; }
    public required string Country { get; init; }
    public required int RoundNumber { get; init; }
    public required int Year { get; init; }
}