using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PlantDiseaseApi.DTOs
{
    public class PredictionRequestDto
    {
        [Required(ErrorMessage = "Görüntü dosyası gereklidir.")]
        public IFormFile File { get; set; } = default!;
    }
}