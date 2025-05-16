namespace PariPlay.Models.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int MatchesPlayed { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int Points { get; set; }
}
