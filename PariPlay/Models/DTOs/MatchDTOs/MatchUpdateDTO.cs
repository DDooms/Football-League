using System.ComponentModel.DataAnnotations;

namespace PariPlay.Models.DTOs.MatchDTOs;

public class MatchUpdateDTO
{
    [Required]
    public int HomeTeamScore { get; set; }
    [Required]
    public int AwayTeamScore { get; set; }
    [Required]
    public DateTime PlayedAt { get; set; }
}