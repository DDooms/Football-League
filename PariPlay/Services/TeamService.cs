using PariPlay.Models.DTOs.TeamDTOs;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Services.Interfaces;

namespace PariPlay.Services;

public class TeamService(ITeamRepository teamRepository) : ITeamService
{
    public async Task<List<TeamResponseDTO>> GetAllTeamsAsync()
    {
        var teams = await teamRepository.GetAllAsync();
        return teams.Select(t => new TeamResponseDTOBuilder()
                .SetId(t.Id)
                .SetName(t.Name)
                .SetMatchesPlayed(t.MatchesPlayed)
                .SetWins(t.Wins)
                .SetDraws(t.Draws)
                .SetLosses(t.Losses)
                .SetPoints(t.Points)
                .Build())
            .ToList();
    }

    public async Task<TeamResponseDTO?> GetTeamByIdAsync(int id)
    {
        var t = await teamRepository.GetByIdAsync(id);
        if (t == null) return null;

        return new TeamResponseDTOBuilder()
            .SetId(t.Id)
            .SetName(t.Name)
            .SetMatchesPlayed(t.MatchesPlayed)
            .SetWins(t.Wins)
            .SetDraws(t.Draws)
            .SetLosses(t.Losses)
            .SetPoints(t.Points)
            .Build();
    }

    public async Task<TeamResponseDTO> AddTeamAsync(TeamCreateDTO dto)
    {
        var team = new Team { Name = dto.Name };
        await teamRepository.AddAsync(team);

        return new TeamResponseDTOBuilder()
            .SetId(team.Id)
            .SetName(team.Name)
            .SetMatchesPlayed(0)
            .SetWins(0)
            .SetDraws(0)
            .SetLosses(0)
            .SetPoints(0)
            .Build();
    }

    public async Task<bool> UpdateTeamAsync(int id, TeamUpdateDTO dto)
    {
        var team = await teamRepository.GetByIdAsync(id);
        if (team == null) return false;

        team.Name = dto.Name;
        await teamRepository.UpdateAsync(team);
        return true;
    }

    public async Task DeleteTeamAsync(int id)
    {
        await teamRepository.DeleteAsync(id);
    }
}