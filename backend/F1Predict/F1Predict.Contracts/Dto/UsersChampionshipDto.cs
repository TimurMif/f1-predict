namespace F1Predict.Contracts.Dto;

public record UsersChampionshipDto(
    int Id,
    int Year,
    int CreatedByUserId,
    string InviteCode,
    List<UserInChampionshipDto> Users);