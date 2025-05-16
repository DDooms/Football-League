namespace PariPlay.Models.DTOs.TeamDTOs;

public class TeamResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MatchesPlayed { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int Points { get; set; }
}