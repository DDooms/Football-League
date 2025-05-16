using PariPlay.Models.Entities;

namespace PariPlay.Strategies.Interfaces;

public interface IMatchProcessor
{
    Task ProcessMatchAsync(Match match, Team home, Team away);
}