namespace F1Predict.Contracts.Dto;

public record UserInChampionshipEditDto
{
    public int ChampionshipId { get; init; }
    public int UserId { get; init; }
}