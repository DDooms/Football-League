using Microsoft.AspNetCore.Mvc;
using PariPlay.Models.DTOs.MatchDTOs;
using PariPlay.Services.Interfaces;

namespace PariPlay.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController(IMatchService matchService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var matches = await matchService.GetAllMatchesAsync();
            return Ok(matches);
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
            var match = await matchService.GetMatchByIdAsync(id);
            return match == null ? NotFound() : Ok(match);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MatchCreateDTO dto)
    {
        try
        {
            var created = await matchService.AddMatchAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MatchUpdateDTO dto)
    {
        try
        {
            var updated = await matchService.UpdateMatchAsync(id, dto);
            return Ok(updated);
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
            await matchService.DeleteMatchAsync(id);
            return Ok(new { Message = "Match deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}