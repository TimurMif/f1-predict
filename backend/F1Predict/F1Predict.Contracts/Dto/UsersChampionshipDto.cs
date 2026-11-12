namespace F1Predict.Contracts.Dto;

public record UsersChampionshipDto
{
    public int Id { get; init; }
    public int Year { get; init; }
    public required string InviteCode { get; init; }
    public required List<UserInChampionshipDto> Users { get; init; }
}