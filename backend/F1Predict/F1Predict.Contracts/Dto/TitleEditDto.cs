namespace F1Predict.Contracts.Dto;

public record TitleEditDto
{
    public int Id { get; init; }
    public required int Year { get; init; }
}