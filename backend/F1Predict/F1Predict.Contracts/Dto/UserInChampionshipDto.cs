namespace F1Predict.Contracts.Dto;

public record UserInChampionshipDto
{
    public int Id { get; init; }
    public int ChampionshipId { get; init; }
    public int UserId { get; init; }
    public int TotalPoints { get; init; }
    public DateTime JoinedAt { get; init; }
}