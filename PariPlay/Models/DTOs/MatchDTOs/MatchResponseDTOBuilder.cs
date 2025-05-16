using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlay.Models.DTOs.MatchDTOs;

public class MatchResponseDTOBuilder
{
    private readonly MatchResponseDTO _dto = new();

    public MatchResponseDTOBuilder SetId(int id)
    {
        _dto.Id = id;
        return this;
    }

    public MatchResponseDTOBuilder SetHomeTeam(int id, string name)
    {
        _dto.HomeTeamId = id;
        _dto.HomeTeam = name;
        return this;
    }

    public MatchResponseDTOBuilder SetAwayTeam(int id, string name)
    {
        _dto.AwayTeamId = id;
        _dto.AwayTeam = name;
        return this;
    }

    public MatchResponseDTOBuilder SetScore(int home, int away)
    {
        _dto.HomeTeamScore = home;
        _dto.AwayTeamScore = away;
        return this;
    }

    public MatchResponseDTOBuilder SetDate(DateTime playedAt)
    {
        _dto.PlayedAt = playedAt;
        return this;
    }
    
    public MatchResponseDTOBuilder SetMatchType(MatchType matchType)
    {
        _dto.MatchType = matchType;
        return this;
    }

    public MatchResponseDTO Build()
    {
        return _dto;
    }
}