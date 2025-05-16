using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Strategies.Interfaces;

namespace PariPlay.Strategies;

public class FriendlyMatchStrategy : IMatchStrategy
{
    public Task ProcessMatchAsync(Match match, Team home, Team away, ITeamRepository teamRepository)
    {
        // Friendly match: no need to update any stats
        return Task.CompletedTask;
    }
}