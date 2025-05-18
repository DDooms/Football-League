using Microsoft.AspNetCore.Mvc;
using PariPlay.Models.DTOs.TeamDTOs;
using PariPlay.Services.Interfaces;

namespace PariPlay.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController(ITeamService teamService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var teams = await teamService.GetAllTeamsAsync();
            return Ok(teams);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var team = await teamService.GetTeamByIdAsync(id);
            return team != null ? Ok(team) : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeamCreateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await teamService.AddTeamAsync(dto);
            return Ok(new { Message = "Team created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TeamUpdateDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await teamService.UpdateTeamAsync(id, dto);
            return Ok(new { Message = "Team updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await teamService.DeleteTeamAsync(id);
            return Ok(new { Message = "Team deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}