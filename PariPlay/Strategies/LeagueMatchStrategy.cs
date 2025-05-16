using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Strategies.Interfaces;

namespace PariPlay.Strategies;

public class LeagueMatchStrategy : IMatchStrategy
{
    public async Task ProcessMatchAsync(Match match, Team home, Team away, ITeamRepository teamRepository)
    {
        home.MatchesPlayed++;
        away.MatchesPlayed++;

        if (match.HomeTeamScore > match.AwayTeamScore)
        {
            home.Wins++;
            away.Losses++;
            home.Points += 3;
        }
        else if (match.AwayTeamScore > match.HomeTeamScore)
        {
            away.Wins++;
            home.Losses++;
            away.Points += 3;
        }
        else
        {
            home.Draws++;
            away.Draws++;
            home.Points += 1;
            away.Points += 1;
        }

        await teamRepository.UpdateAsync(home);
        await teamRepository.UpdateAsync(away);
    }
}