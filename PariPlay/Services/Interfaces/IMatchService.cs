using PariPlay.Models.DTOs.MatchDTOs;

namespace PariPlay.Services.Interfaces;

public interface IMatchService
{
    Task<List<MatchResponseDTO>> GetAllMatchesAsync();
    Task<MatchResponseDTO?> GetMatchByIdAsync(int id);
    Task<MatchResponseDTO> AddMatchAsync(MatchCreateDTO match); // Will also update team points
    Task<bool> UpdateMatchAsync(int id, MatchUpdateDTO match);
    Task DeleteMatchAsync(int id);
}