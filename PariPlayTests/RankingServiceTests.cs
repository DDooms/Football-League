using Moq;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Services;
using Match = PariPlay.Models.Entities.Match;

namespace PariPlayTests;

[TestClass]
public class RankingServiceTests
{
    private Mock<ITeamRepository> _mockTeamRepo;
    private Mock<IMatchRepository> _mockMatchRepo;
    private RankingService _service;

    [TestInitialize]
    public void Setup()
    {
        _mockTeamRepo = new Mock<ITeamRepository>();
        _mockMatchRepo = new Mock<IMatchRepository>();
        _service = new RankingService(_mockTeamRepo.Object, _mockMatchRepo.Object);
    }

    [TestMethod]
    public async Task GetRankingAsync_ShouldReturnSortedRanking()
    {
        var teams = new List<Team>
        {
            new() { Id = 1, Name = "Team A", Wins = 2, Draws = 1, Losses = 0, MatchesPlayed = 3, Points = 7 },
            new() { Id = 2, Name = "Team B", Wins = 1, Draws = 2, Losses = 0, MatchesPlayed = 3, Points = 5 }
        };

        var matches = new List<Match>
        {
            new() { Id = 1, HomeTeamId = 1, AwayTeamId = 2, HomeTeamScore = 2, AwayTeamScore = 1 },
            new() { Id = 2, HomeTeamId = 2, AwayTeamId = 1, HomeTeamScore = 0, AwayTeamScore = 3 }
        };

        _mockTeamRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(teams);
        _mockMatchRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(matches);

        var result = await _service.GetRankingAsync();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Team A", result[0].TeamName);
        Assert.AreEqual(1, result[0].Position);
        Assert.AreEqual("Team B", result[1].TeamName);
        Assert.AreEqual(2, result[1].Position);
    }
}