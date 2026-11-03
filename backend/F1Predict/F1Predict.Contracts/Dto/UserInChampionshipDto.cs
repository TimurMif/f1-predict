namespace F1Predict.Contracts.Dto;

public record UserInChampionshipDto(
    int Id,
    int ChampionshipId,
    int UserId,
    string UserName,
    int TotalPoints,
    DateTime JoinedAt);