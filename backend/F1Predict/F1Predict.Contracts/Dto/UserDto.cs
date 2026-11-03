namespace F1Predict.Contracts.Dto;

public record UserDto(
    int Id,
    string FirstName,
    string? LastName,
    int Points,
    List<TitleDto> Titles,
    int? ChampionshipId);