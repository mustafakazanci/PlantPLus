using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Data;
using PlantDiseaseApi.DTOs;

namespace PlantDiseaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiseaseController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseaseDto>>> GetDiseases()
        {
            return await _context.Diseases
                .Include(d => d.DiseaseSolutions)!.ThenInclude(ds => ds.Solution)
                .Select(d => new DiseaseDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Symptoms = d.Symptoms,
                    Cause = d.Cause,
                    Prevention = d.Prevention, 
                    ImageUrl = d.ImageUrl,
                    SuggestedSolutions = d.DiseaseSolutions!.Select(ds => new SolutionDto
                    {
                        Id = ds.Solution!.Id,
                        Title = ds.Solution.Title,
                        Description = ds.Solution.Description,
                        ReferenceUrl = ds.Solution.ReferenceUrl
                    }).ToList()
                })
                .ToListAsync();
        }
        
    }
}