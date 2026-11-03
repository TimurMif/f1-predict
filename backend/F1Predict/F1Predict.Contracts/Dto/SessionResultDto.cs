namespace F1Predict.Contracts.Dto;

/// <summary>
/// Результат одного пилота в одной сессии.
/// BestLapTimeMs — время круга в миллисекундах (84758 = 1:24.758).
/// </summary>
public record SessionResultDto(
    int Id,
    int SessionId,
    DriverDto Driver,
    int Position,
    int? BestLapTimeMs);