using Microsoft.EntityFrameworkCore;
using PariPlay.Data;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;

namespace PariPlay.Repositories;

public class TeamRepository(ApplicationDbContext context) : ITeamRepository
{
    public Task<List<Team>> GetAllAsync() => context.Teams.ToListAsync();

    public Task<Team?> GetByIdAsync(int id) => context.Teams.FindAsync(id).AsTask();

    public async Task AddAsync(Team team)
    {
        await context.Teams.AddAsync(team);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Team team)
    {
        context.Teams.Update(team);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var team = await context.Teams.FindAsync(id);
        if (team is not null)
        {
            context.Teams.Remove(team);
            await context.SaveChangesAsync();
        }
    }
}