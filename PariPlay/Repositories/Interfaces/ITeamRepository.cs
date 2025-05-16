using PariPlay.Models.Entities;

namespace PariPlay.Repositories.Interfaces;

public interface ITeamRepository
{
    Task<List<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(int id);
    Task AddAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(int id);
}