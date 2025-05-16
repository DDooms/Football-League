using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;

namespace PariPlay.Strategies.Interfaces;

public interface IMatchStrategy
{
    Task ProcessMatchAsync(Match match, Team home, Team away, ITeamRepository teamRepository);
}