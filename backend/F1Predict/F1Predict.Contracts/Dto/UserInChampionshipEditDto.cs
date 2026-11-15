namespace F1Predict.Contracts.Dto;
using System.ComponentModel.DataAnnotations;


public record UserInChampionshipEditDto
{
    [Required(ErrorMessage = "Код приглашения обязателен")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Код приглашения — ровно 6 символов")]
    public required string InviteCode { get; init; }
}