namespace F1Predict.Contracts.Dto;

/// <summary>
/// Type: Qualifying, Race, Sprint, SprintQualifying, FreePractice.
/// Status: Scheduled, InProgress, Completed, Cancelled.
/// В DTO это строки, в сущность они превращаются при маппинге.
/// </summary>
public record SessionDto(
    int Id,
    int GrandPrixId,
    string Name,
    string Type,
    DateTime DateStart,
    DateTime DateEnd,
    string Status);