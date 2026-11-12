namespace F1Predict.Contracts.Dto;

public record ConstructorEditDto
{
    public required string Name { get; init; }
    public required string Nationality { get; init; }
    public int Points { get; init; }
}