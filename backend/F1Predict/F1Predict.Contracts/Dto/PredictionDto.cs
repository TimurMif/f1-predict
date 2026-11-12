namespace F1Predict.Contracts.Dto;

public record PredictionDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int SessionId { get; init; }
    public int DriverTopOneId { get; init; }
    public int DriverTopTwoId { get; init; }
    public int DriverTopThreeId { get; init; }
    public int? BestSessionLapTimeMs { get; init; }
    public int TotalPointsForThisPrediction { get; init; }
    public DateTime Created { get; init; }
    public DateTime? Edited { get; init; }
}