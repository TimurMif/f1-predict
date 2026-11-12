namespace F1Predict.Contracts.Dto;

public record UsersChampionshipEditDto
{
    public int CreatedByUserId { get; set; }
    public int Year { get; set; }
}