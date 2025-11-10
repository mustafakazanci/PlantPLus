using Microsoft.AspNetCore.Mvc;
using PlantDiseaseApi.DTOs;
using PlantDiseaseApi.Services;

namespace PlantDiseaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IMLService _mlService;
        private readonly ILogger<PredictionController> _logger;

        public PredictionController(IMLService mlService, ILogger<PredictionController> logger)
        {
            _mlService = mlService;
            _logger = logger;
        }

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> Predict([FromForm] PredictionRequestDto request)
        {
            // --- HATA DÜZELTME: request.ImageFile -> request.File ---
            var allowedTypes = new[] { "image/jpeg", "image/png" };
            if (!allowedTypes.Contains(request.File.ContentType.ToLower()))
            {
                return BadRequest(new { message = "Sadece .jpeg veya .png formatı desteklenmektedir." });
            }

            // --- HATA DÜZELTME: request.ImageFile -> request.File ---
            _logger.LogInformation($"Yeni tahmin isteği alındı: {request.File.FileName}");
            
            // --- HATA DÜZELTME: request.ImageFile -> request.File ---
            var result = await _mlService.PredictAsync(request.File); 

            if (!result.IsSuccess)
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }

            return Ok(result);
        }
    }
}