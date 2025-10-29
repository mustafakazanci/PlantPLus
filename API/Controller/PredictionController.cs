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

        public PredictionController(IMLService mlService)
        {
            _mlService = mlService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PredictionResponseDto>> Predict([FromForm] PredictionRequestDto request)
        {

            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                return BadRequest(new { Message = "Görüntü dosyası yüklenmelidir. Lütfen geçerli bir bitki yaprağı görseli seçin." });
            }

            var result = await _mlService.PredictDiseaseAsync(request.ImageFile);


            if (result == null)
            {
                 return StatusCode(500, new { Message = "Görüntü işlenirken veya tahmin yapılırken sunucu tarafında beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin." });
            }

         
            return Ok(result);
        }
    }
}