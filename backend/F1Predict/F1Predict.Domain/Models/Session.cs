namespace F1Predict.Domain.Models;

public class Session
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int GrandPrixId { get; set; }
    public required GrandPrix GrandPrix { get; set; }
    public SessionType Type { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public required SessionStatus Status { get; set; }
    public ICollection<SessionResult> Results { get; set; }= new List<SessionResult>();
}