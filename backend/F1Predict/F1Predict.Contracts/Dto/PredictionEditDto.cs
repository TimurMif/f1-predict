namespace F1Predict.Contracts.Dto;

/// <summary>
/// UserId временно передаётся в теле; после добавления авторизации его нужно брать из текущего пользователя.
/// </summary>
public record PredictionEditDto(
    int UserId,
    int SessionId,
    int DriverTopOneId,
    int DriverTopTwoId,
    int DriverTopThreeId,
    int? BestSessionLapTimeMs);