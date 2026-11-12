using F1Predict.Domain.Models;

namespace F1Predict.Contracts.Dto;

/// <summary>
/// Type: Qualifying, Race, Sprint, SprintQualifying, FreePractice.
/// Status: Scheduled, InProgress, Completed, Cancelled.
/// В DTO это строки, в сущность они превращаются при маппинге.
/// </summary>
public record SessionDto
{
    public int Id { get; init; }
    public int GrandPrixId { get; init; }
    public required string Name { get; init; }
    public required SessionType Type { get; init; }
    public DateTime DateStart { get; init; }
    public DateTime DateEnd { get; init; }
    public required SessionStatus Status { get; init; }
}