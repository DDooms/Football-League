using System.Text.Json.Serialization;
using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlay.Models.DTOs.MatchDTOs;

public class MatchResponseDTO
{
    public int Id { get; set; }
    public string HomeTeam { get; set; } = string.Empty;
    public int HomeTeamId { get; set; }
    public string AwayTeam { get; set; } = string.Empty;
    public int AwayTeamId { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
    public DateTime PlayedAt { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MatchType MatchType { get; set; }
}