using PariPlay.Models.DTOs.RankingDTOs;
using PariPlay.Repositories.Interfaces;
using PariPlay.Services.Interfaces;
using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlay.Services;

public class RankingService(ITeamRepository teamRepository, IMatchRepository matchRepository)
    : IRankingService
{
    public async Task<List<RankingDTO>> GetRankingAsync()
    {
        var teams = await teamRepository.GetAllAsync();
        var matches = await matchRepository.GetAllAsync();

        var rankings = teams.Select(team =>
            {
                var teamMatches = matches
                    .Where(m => (m.HomeTeamId == team.Id || m.AwayTeamId == team.Id) 
                                && m.MatchType == MatchType.League);

                int goalsScored = teamMatches.Sum(m =>
                    m.HomeTeamId == team.Id ? m.HomeTeamScore :
                    m.AwayTeamId == team.Id ? m.AwayTeamScore : 0);

                int goalsConceded = teamMatches.Sum(m =>
                    m.HomeTeamId == team.Id ? m.AwayTeamScore :
                    m.AwayTeamId == team.Id ? m.HomeTeamScore : 0);

                return new RankingDTOBuilder()
                    .SetTeamName(team.Name)
                    .SetMatchesPlayed(team.MatchesPlayed)
                    .SetWins(team.Wins)
                    .SetDraws(team.Draws)
                    .SetLosses(team.Losses)
                    .SetGoalDifference(goalsScored - goalsConceded)
                    .SetPoints(team.Points)
                    .Build();
            })
            .OrderByDescending(r => r.Points)
            .ThenByDescending(r => r.GoalDifference)
            .ToList();

        for (int i = 0; i < rankings.Count; i++)
            rankings[i].Position = i + 1;

        return rankings;
    }
}