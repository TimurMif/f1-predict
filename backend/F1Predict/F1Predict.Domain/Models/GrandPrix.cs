namespace F1Predict.Domain.Models;

public class GrandPrix
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string CircuitName { get; set; }
    public required string Country { get; set; }
    public required int RoundNumber { get; set; }
    public required int Year { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}