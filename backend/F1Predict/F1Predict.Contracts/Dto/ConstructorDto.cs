namespace F1Predict.Contracts.Dto;

public record ConstructorDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Nationality { get; init; }
    public required int Points { get; init; }
}