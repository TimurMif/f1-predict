namespace F1Predict.Contracts.Dto;

public record UserDto
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required int Points { get; init; }
    public required List<TitleDto> Titles { get; init; }
    public int ChampionshipId { get; init; }
}