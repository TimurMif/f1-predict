namespace F1Predict.Contracts.Dto;

/// <summary>
/// Результат одного пилота в одной сессии.
/// BestLapTimeMs — время круга в миллисекундах (84758 = 1:24.758).
/// </summary>
public record DriverResultDto
{
    public int Id { get; init; }
    public int SessionId {get; init; }
    public required DriverDto Driver { get; init; }
    public int Position { get; init; }
    public int? BestLapTimeMs { get; init; }
}