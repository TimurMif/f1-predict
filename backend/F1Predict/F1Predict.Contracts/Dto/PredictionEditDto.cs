namespace F1Predict.Contracts.Dto;

public record PredictionEditDto
{
    public int SessionId { get; init; }
    public int DriverTopOneId { get; init; }
    public int DriverTopTwoId { get; init; }
    public int DriverTopThreeId { get; init; }
    public int? BestSessionLapMs { get; init; }
}