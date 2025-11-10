using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Data;
using PlantDiseaseApi.Models;
namespace PlantDiseaseApi.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class TipsController : ControllerBase
{
private readonly ApplicationDbContext _context;

public TipsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{plantName}")]
    public async Task<ActionResult<IEnumerable<Tip>>> GetTipsForPlant(string plantName)
    {
        if (string.IsNullOrEmpty(plantName))
        {
            return BadRequest("Bitki adı belirtilmelidir.");
        }

        var tips = await _context.Tips
            .Where(t => t.PlantName.ToLower() == plantName.ToLower())
            .ToListAsync();

        if (tips == null || !tips.Any())
        {
            return NotFound($"{plantName} için ipucu bulunamadı.");
        }

        return Ok(tips);
    }
}
}