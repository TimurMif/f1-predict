namespace F1Predict.Contracts.Dto;

public record GrandPrixDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string CircuitName { get; init; }
    public required string Country { get; init; }
    public required int Year { get; init; }
    public required List<SessionDto> Sessions { get; init; }
}