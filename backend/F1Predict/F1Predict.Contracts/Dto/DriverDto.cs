namespace F1Predict.Contracts.Dto;

public record DriverDto
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Number { get; init; }
    public required string Nationality { get; init; }
    public int Points { get; init; }
    public required ConstructorDto Constructor { get; init; }
}