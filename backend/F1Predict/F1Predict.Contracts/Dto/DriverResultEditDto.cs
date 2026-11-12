namespace F1Predict.Contracts.Dto;

public record DriverResultEditDto
{
    public int SessionId { get; init; }
    public int DriverId { get; init; }
    public int Position {get; init; }
    public int? BestLapTimeMs { get; init; }
}