using Moq;
using PariPlay.Models.DTOs.MatchDTOs;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Services;
using PariPlay.Strategies.Interfaces;
using Match = PariPlay.Models.Entities.Match;
using MatchType = PariPlay.Models.Enums.MatchType;

namespace PariPlayTests;

[TestClass]
public class MatchServiceTests
{
    private Mock<IMatchRepository> _mockMatchRepo;
    private Mock<ITeamRepository> _mockTeamRepo;
    private MatchService _service;

    private Mock<IMatchProcessor> _mockProcessor;

    [TestInitialize]
    public void Setup()
    {
        _mockMatchRepo = new Mock<IMatchRepository>();
        _mockTeamRepo = new Mock<ITeamRepository>();
        _mockProcessor = new Mock<IMatchProcessor>();
        _service = new MatchService(_mockMatchRepo.Object, _mockTeamRepo.Object, _mockProcessor.Object);
    }


    [TestMethod]
    public async Task GetAllMatchesAsync_ShouldReturnMatchList()
    {
        var matches = new List<Match>
        {
            new Match
            {
                Id = 1,
                HomeTeam = new Team { Id = 1, Name = "Team A" },
                AwayTeam = new Team { Id = 2, Name = "Team B" },
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeTeamScore = 1,
                AwayTeamScore = 2,
                PlayedAt = DateTime.Now
            }
        };

        _mockMatchRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(matches);

        var result = await _service.GetAllMatchesAsync();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Team A", result[0].HomeTeam);
    }

    [TestMethod]
    public async Task GetMatchByIdAsync_ShouldReturnMatch_WhenExists()
    {
        var match = new Match
        {
            Id = 1,
            HomeTeamId = 1,
            AwayTeamId = 2,
            HomeTeam = new Team { Name = "A" },
            AwayTeam = new Team { Name = "B" },
            HomeTeamScore = 2,
            AwayTeamScore = 3,
            PlayedAt = DateTime.Today
        };

        _mockMatchRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

        var result = await _service.GetMatchByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("A", result.HomeTeam);
    }

    [TestMethod]
    public async Task GetMatchByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        _mockMatchRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Match)null!);

        var result = await _service.GetMatchByIdAsync(1);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddMatchAsync_ShouldReturnCreatedMatchDTO()
    {
        var dto = new MatchCreateDTO
        {
            HomeTeamId = 1,
            AwayTeamId = 2,
            HomeTeamScore = 2,
            AwayTeamScore = 1,
            PlayedAt = DateTime.Now,
            MatchType = MatchType.League
        };

        var homeTeam = new Team { Id = 1, Name = "Team A" };
        var awayTeam = new Team { Id = 2, Name = "Team B" };

        _mockTeamRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(homeTeam);
        _mockTeamRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(awayTeam);
        _mockMatchRepo.Setup(r => r.AddAsync(It.IsAny<Match>())).Returns(Task.CompletedTask);
        _mockProcessor.Setup(p => p.ProcessMatchAsync(It.IsAny<Match>(), homeTeam, awayTeam))
            .Returns(Task.CompletedTask);

        var result = await _service.AddMatchAsync(dto);

        Assert.AreEqual("Team A", result.HomeTeam);
        _mockProcessor.Verify(p => p.ProcessMatchAsync(It.IsAny<Match>(), homeTeam, awayTeam), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Invalid team IDs.")]
    public async Task AddMatchAsync_ShouldThrowException_WhenTeamsAreInvalid()
    {
        var dto = new MatchCreateDTO { HomeTeamId = 1, AwayTeamId = 2 };

        _mockTeamRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Team)null!);
        _mockTeamRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Team)null!);

        await _service.AddMatchAsync(dto);
    }

    [TestMethod]
    public async Task UpdateMatchAsync_ShouldReturnTrue_WhenMatchExists()
    {
        var match = new Match { Id = 1 };
        var dto = new MatchUpdateDTO { HomeTeamScore = 1, AwayTeamScore = 2, PlayedAt = DateTime.Now };

        _mockMatchRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);
        _mockMatchRepo.Setup(r => r.UpdateAsync(It.IsAny<Match>())).Returns(Task.CompletedTask);

        var result = await _service.UpdateMatchAsync(1, dto);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task UpdateMatchAsync_ShouldReturnFalse_WhenMatchNotFound()
    {
        _mockMatchRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Match)null!);

        var result = await _service.UpdateMatchAsync(1, new MatchUpdateDTO());

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task DeleteMatchAsync_ShouldCallRepositoryDelete()
    {
        await _service.DeleteMatchAsync(5);

        _mockMatchRepo.Verify(r => r.DeleteAsync(5), Times.Once);
    }
}