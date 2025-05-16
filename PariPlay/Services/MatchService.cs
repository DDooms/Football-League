using PariPlay.Models.DTOs.MatchDTOs;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Services.Interfaces;
using PariPlay.Strategies.Interfaces;

namespace PariPlay.Services;

public class MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, IMatchProcessor _matchProcessor)
    : IMatchService
{
    public async Task<List<MatchResponseDTO>> GetAllMatchesAsync()
    {
        var matches = await matchRepository.GetAllAsync();
        var responses = new List<MatchResponseDTO>();

        foreach (var m in matches)
        {
            var home = m.HomeTeam;
            var away = m.AwayTeam;

            var response = new MatchResponseDTOBuilder()
                .SetId(m.Id)
                .SetHomeTeam(home.Id, home.Name)
                .SetAwayTeam(away.Id, away.Name)
                .SetScore(m.HomeTeamScore, m.AwayTeamScore)
                .SetDate(m.PlayedAt)
                .SetMatchType(m.MatchType)
                .Build();

            responses.Add(response);
        }

        return responses;
    }

    public async Task<MatchResponseDTO?> GetMatchByIdAsync(int id)
    {
        var m = await matchRepository.GetByIdAsync(id);
        if (m == null) return null;

        var response = new MatchResponseDTOBuilder()
            .SetId(m.Id)
            .SetHomeTeam(m.HomeTeam.Id, m.HomeTeam.Name)
            .SetAwayTeam(m.AwayTeam.Id, m.AwayTeam.Name)
            .SetScore(m.HomeTeamScore, m.AwayTeamScore)
            .SetDate(m.PlayedAt)
            .SetMatchType(m.MatchType)
            .Build();

        return response;
    }

    public async Task<MatchResponseDTO> AddMatchAsync(MatchCreateDTO dto)
    {
        var home = await teamRepository.GetByIdAsync(dto.HomeTeamId);
        var away = await teamRepository.GetByIdAsync(dto.AwayTeamId);

        if (home == null || away == null)
            throw new Exception("Invalid team IDs.");

        var match = new Match
        {
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            HomeTeamScore = dto.HomeTeamScore,
            AwayTeamScore = dto.AwayTeamScore,
            PlayedAt = dto.PlayedAt,
            MatchType = dto.MatchType
        };

        await matchRepository.AddAsync(match);

        // Use strategy pattern here to process the match based on its type
        await _matchProcessor.ProcessMatchAsync(match, home, away);
        
        var response = new MatchResponseDTOBuilder()
            .SetId(match.Id)
            .SetHomeTeam(home.Id, home.Name)
            .SetAwayTeam(away.Id, away.Name)
            .SetScore(match.HomeTeamScore, match.AwayTeamScore)
            .SetDate(match.PlayedAt)
            .SetMatchType(match.MatchType)
            .Build();

        return response;
    }

    public async Task<bool> UpdateMatchAsync(int id, MatchUpdateDTO dto)
    {
        var match = await matchRepository.GetByIdAsync(id);
        if (match == null) return false;

        match.HomeTeamScore = dto.HomeTeamScore;
        match.AwayTeamScore = dto.AwayTeamScore;
        match.PlayedAt = dto.PlayedAt;

        await matchRepository.UpdateAsync(match);
        return true;
    }
    
    public async Task DeleteMatchAsync(int id)
    {
        await matchRepository.DeleteAsync(id);
    }
}