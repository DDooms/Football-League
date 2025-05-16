using Microsoft.EntityFrameworkCore;
using PariPlay.Data;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;

namespace PariPlay.Repositories;

public class MatchRepository(ApplicationDbContext context) : IMatchRepository
{
    public async Task<List<Match>> GetAllAsync()
    {
        return await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .ToListAsync();
    }

    public async Task<Match?> GetByIdAsync(int id)
    {
        return await context.Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Match match)
    {
        await context.Matches.AddAsync(match);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Match match)
    {
        context.Matches.Update(match);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var match = await context.Matches.FindAsync(id);
        if (match is not null)
        {
            context.Matches.Remove(match);
            await context.SaveChangesAsync();
        }
    }
}