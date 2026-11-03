namespace F1Predict.Contracts.Dto;

public record PredictionDto(
    int Id,
    int UserId,
    int SessionId,
    int DriverTopOneId,
    int DriverTopTwoId,
    int DriverTopThreeId,
    int? BestSessionLapTimeMs,
    int TotalPointsForThisPrediction,
    DateTime Created,
    DateTime? Edited);