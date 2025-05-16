using PariPlay.Models.DTOs;
using PariPlay.Models.DTOs.TeamDTOs;

namespace PariPlay.Services.Interfaces;

public interface ITeamService
{
    Task<List<TeamResponseDTO>> GetAllTeamsAsync();
    Task<TeamResponseDTO?> GetTeamByIdAsync(int id);
    Task<TeamResponseDTO> AddTeamAsync(TeamCreateDTO team);
    Task<bool> UpdateTeamAsync(int id, TeamUpdateDTO team);
    Task DeleteTeamAsync(int id);
}