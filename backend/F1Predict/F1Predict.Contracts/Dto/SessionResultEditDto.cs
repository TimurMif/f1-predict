namespace F1Predict.Contracts.Dto;

public record SessionResultEditDto(
    int SessionId,
    int DriverId,
    int Position,
    int? BestLapTimeMs);