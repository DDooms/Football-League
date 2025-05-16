using PariPlay.Models.DTOs.RankingDTOs;

namespace PariPlay.Services.Interfaces;

public interface IRankingService
{
    Task<List<RankingDTO>> GetRankingAsync();
}