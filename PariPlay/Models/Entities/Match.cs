using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlay.Models.Entities;

public class Match
{
    public int Id { get; set; }
    
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; }

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; }

    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }

    public DateTime PlayedAt { get; set; }
    public MatchType MatchType { get; set; }
}
