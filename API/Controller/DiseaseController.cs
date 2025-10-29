using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Data;
using PlantDiseaseApi.DTOs;
using PlantDiseaseApi.Models;

namespace PlantDiseaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiseaseController(ApplicationDbContext context)
        {
            _context = context;
        }


       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseaseDto>>> GetDiseases()
        {
           
            return await _context.Diseases
                .Include(d => d.DiseaseSolutions)!
                .ThenInclude(ds => ds.Solution)
                .Select(d => new DiseaseDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Symptoms = d.Symptoms,
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


        [HttpGet("{id}")]
        public async Task<ActionResult<DiseaseDto>> GetDisease(int id) // Dönüş 
        {
            var disease = await _context.Diseases
                .Include(d => d.DiseaseSolutions)!
                .ThenInclude(ds => ds.Solution)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (disease == null)
            {
                return NotFound();
            }


            var diseaseDto = new DiseaseDto
            {
                Id = disease.Id,
                Name = disease.Name,
                Description = disease.Description,
                Symptoms = disease.Symptoms,
                ImageUrl = disease.ImageUrl,
                SuggestedSolutions = disease.DiseaseSolutions!.Select(ds => new SolutionDto
                {
                    Id = ds.Solution!.Id,
                    Title = ds.Solution.Title,
                    Description = ds.Solution.Description,
                    ReferenceUrl = ds.Solution.ReferenceUrl
                }).ToList()
            };

            return diseaseDto;
        }

        // Not: Hastalık verileri statik olacağı için POST, PUT, DELETE metotları eklemiyoruz.
    }
}