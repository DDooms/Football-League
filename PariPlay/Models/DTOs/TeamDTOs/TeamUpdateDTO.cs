using System.ComponentModel.DataAnnotations;

namespace PariPlay.Models.DTOs.TeamDTOs;

public class TeamUpdateDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
}