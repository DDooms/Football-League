using PariPlay.Models.Entities;

namespace PariPlay.Repositories.Interfaces;

public interface IMatchRepository
{
    Task<List<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task AddAsync(Match match);
    Task UpdateAsync(Match match);
    Task DeleteAsync(int id);
}