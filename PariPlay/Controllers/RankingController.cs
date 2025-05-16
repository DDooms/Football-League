using Microsoft.AspNetCore.Mvc;
using PariPlay.Services.Interfaces;

namespace PariPlay.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RankingController(IRankingService rankingService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRanking()
    {
        try
        {
            var ranking = await rankingService.GetRankingAsync();
            return Ok(ranking);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });;
        }
    }
}