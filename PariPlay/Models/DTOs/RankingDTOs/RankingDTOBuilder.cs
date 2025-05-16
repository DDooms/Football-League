namespace PariPlay.Models.DTOs.RankingDTOs;

public class RankingDTOBuilder
{
    private readonly RankingDTO _ranking = new();

    public RankingDTOBuilder SetTeamName(string name)
    {
        _ranking.TeamName = name;
        return this;
    }

    public RankingDTOBuilder SetMatchesPlayed(int matchesPlayed)
    {
        _ranking.MatchesPlayed = matchesPlayed;
        return this;
    }

    public RankingDTOBuilder SetWins(int wins)
    {
        _ranking.Wins = wins;
        return this;
    }

    public RankingDTOBuilder SetDraws(int draws)
    {
        _ranking.Draws = draws;
        return this;
    }

    public RankingDTOBuilder SetLosses(int losses)
    {
        _ranking.Losses = losses;
        return this;
    }

    public RankingDTOBuilder SetGoalDifference(int goalDifference)
    {
        _ranking.GoalDifference = goalDifference;
        return this;
    }

    public RankingDTOBuilder SetPoints(int points)
    {
        _ranking.Points = points;
        return this;
    }

    public RankingDTOBuilder SetPosition(int position)
    {
        _ranking.Position = position;
        return this;
    }

    public RankingDTO Build()
    {
        return _ranking;
    }
}