﻿namespace PariPlay.Models.DTOs.RankingDTOs;

public class RankingDTO
{
    public int Position { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int MatchesPlayed { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalDifference { get; set; }
    public int Points { get; set; }
}