namespace F1Predict.Contracts.Dto;

public record TitleDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public required int Year { get; init; }
}