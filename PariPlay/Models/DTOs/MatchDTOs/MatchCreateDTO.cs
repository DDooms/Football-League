using System.ComponentModel.DataAnnotations;
using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlay.Models.DTOs.MatchDTOs;

public class MatchCreateDTO
{
    [Required]
    public int HomeTeamId { get; set; }
    [Required]
    public int AwayTeamId { get; set; }
    [Required]
    public int HomeTeamScore { get; set; }
    [Required]
    public int AwayTeamScore { get; set; }
    [Required]
    public DateTime PlayedAt { get; set; }
    public MatchType MatchType { get; set; }
}