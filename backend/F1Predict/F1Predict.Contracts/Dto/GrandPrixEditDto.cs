namespace F1Predict.Contracts.Dto;

public record GrandPrixEditDto(
    string Name,
    string CircuitName,
    string Country,
    int RoundNumber,
    int Year);