using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Strategies.Interfaces;
using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlay.Strategies;

public class MatchProcessor(ITeamRepository teamRepository) : IMatchProcessor
{
    public async Task ProcessMatchAsync(Match match, Team home, Team away)
    {
        IMatchStrategy strategy = match.MatchType switch
        {
            MatchType.League => new LeagueMatchStrategy(),
            MatchType.Friendly => new FriendlyMatchStrategy(),
            _ => throw new NotImplementedException("Unknown match type")
        };

        await strategy.ProcessMatchAsync(match, home, away, teamRepository);
    }
}