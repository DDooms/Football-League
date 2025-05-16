namespace PariPlay.Models.DTOs.TeamDTOs;

public class TeamResponseDTOBuilder
{
    private readonly TeamResponseDTO _teamResponse = new();

    public TeamResponseDTOBuilder SetId(int id)
    {
        _teamResponse.Id = id;
        return this;
    }

    public TeamResponseDTOBuilder SetName(string name)
    {
        _teamResponse.Name = name;
        return this;
    }

    public TeamResponseDTOBuilder SetMatchesPlayed(int matchesPlayed)
    {
        _teamResponse.MatchesPlayed = matchesPlayed;
        return this;
    }

    public TeamResponseDTOBuilder SetWins(int wins)
    {
        _teamResponse.Wins = wins;
        return this;
    }

    public TeamResponseDTOBuilder SetDraws(int draws)
    {
        _teamResponse.Draws = draws;
        return this;
    }

    public TeamResponseDTOBuilder SetLosses(int losses)
    {
        _teamResponse.Losses = losses;
        return this;
    }

    public TeamResponseDTOBuilder SetPoints(int points)
    {
        _teamResponse.Points = points;
        return this;
    }

    public TeamResponseDTO Build()
    {
        return _teamResponse;
    }
}