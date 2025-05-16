using Moq;
using PariPlay.Models.DTOs.TeamDTOs;
using PariPlay.Models.Entities;
using PariPlay.Repositories.Interfaces;
using PariPlay.Services;

namespace PariPlayTests;

[TestClass]
public class TeamServiceTests
{
    private Mock<ITeamRepository> _mockTeamRepo;
    private TeamService _service;

    [TestInitialize]
    public void Setup()
    {
        _mockTeamRepo = new Mock<ITeamRepository>();
        _service = new TeamService(_mockTeamRepo.Object);
    }

    [TestMethod]
    public async Task GetAllTeamsAsync_ShouldReturnListOfTeams()
    {
        var teams = new List<Team>
        {
            new Team { Id = 1, Name = "Team A", Wins = 2, Draws = 1, Losses = 0, Points = 7 },
            new Team { Id = 2, Name = "Team B", Wins = 1, Draws = 2, Losses = 0, Points = 5 }
        };

        _mockTeamRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(teams);

        var result = await _service.GetAllTeamsAsync();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Team A", result[0].Name);
        Assert.AreEqual(7, result[0].Points);
    }

    [TestMethod]
    public async Task GetTeamByIdAsync_ShouldReturnTeam_WhenExists()
    {
        var team = new Team { Id = 1, Name = "Team X", Points = 10 };

        _mockTeamRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(team);

        var result = await _service.GetTeamByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Team X", result.Name);
        Assert.AreEqual(10, result.Points);
    }

    [TestMethod]
    public async Task GetTeamByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        _mockTeamRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Team)null!);

        var result = await _service.GetTeamByIdAsync(1);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddTeamAsync_ShouldReturnCreatedTeamDTO()
    {
        var dto = new TeamCreateDTO { Name = "New Team" };

        _mockTeamRepo.Setup(r => r.AddAsync(It.IsAny<Team>())).Callback<Team>(t => t.Id = 5).Returns(Task.CompletedTask);

        var result = await _service.AddTeamAsync(dto);

        Assert.AreEqual("New Team", result.Name);
        Assert.AreEqual(0, result.Points);
        Assert.AreEqual(5, result.Id);
    }

    [TestMethod]
    public async Task UpdateTeamAsync_ShouldReturnTrue_WhenTeamExists()
    {
        var team = new Team { Id = 1, Name = "Old Name" };
        var dto = new TeamUpdateDTO { Name = "New Name" };

        _mockTeamRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(team);
        _mockTeamRepo.Setup(r => r.UpdateAsync(It.IsAny<Team>())).Returns(Task.CompletedTask);

        var result = await _service.UpdateTeamAsync(1, dto);

        Assert.IsTrue(result);
        Assert.AreEqual("New Name", team.Name);
    }

    [TestMethod]
    public async Task UpdateTeamAsync_ShouldReturnFalse_WhenTeamNotFound()
    {
        _mockTeamRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Team)null!);

        var result = await _service.UpdateTeamAsync(1, new TeamUpdateDTO());

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task DeleteTeamAsync_ShouldCallDeleteOnce()
    {
        await _service.DeleteTeamAsync(3);

        _mockTeamRepo.Verify(r => r.DeleteAsync(3), Times.Once);
    }
}