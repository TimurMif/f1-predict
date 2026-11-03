namespace F1Predict.Contracts.Dto;

public record GrandPrixDto(
    int Id,
    string Name,
    string CircuitName,
    string Country,
    int RoundNumber,
    int Year,
    List<SessionDto> Sessions);