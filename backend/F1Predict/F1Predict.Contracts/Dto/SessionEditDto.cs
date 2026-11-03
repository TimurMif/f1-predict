namespace F1Predict.Contracts.Dto;

public record SessionEditDto(
    int GrandPrixId,
    string Name,
    string Type,
    DateTime DateStart,
    DateTime DateEnd,
    string Status);