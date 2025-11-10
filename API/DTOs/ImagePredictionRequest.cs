using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; 
namespace PlantDiseaseApi.DTOs
{
    public class ImagePredictionRequest
    {
        [Required(ErrorMessage = "Görüntü dosyası gereklidir.")]
        public IFormFile ImageFile { get; set; } = default!;

    }
}