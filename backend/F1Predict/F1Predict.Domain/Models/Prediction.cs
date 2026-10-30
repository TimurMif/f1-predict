namespace F1Predict.Domain.Models;

public class Prediction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public int SessionId { get; set; }
    public required Session Session { get; set; }
    public int DriverTopOneId { get; set; }
    public required Driver DriverTopOne { get; set; }
    public int DriverTopTwoId { get; set; }
    public required Driver DriverTopTwo { get; set; }
    public int DriverTopThreeId { get; set; }
    public required Driver DriverTopThree { get; set; }
    public int? BestSessionLapTimeMs { get; set; }
    public int TotalPointsForThisPrediction { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Edited { get; set; }
}