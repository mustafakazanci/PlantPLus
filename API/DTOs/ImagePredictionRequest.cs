using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; 
namespace PlantDiseaseApi.DTOs
{
    // ML tahmini için mobil uygulamadan gelen verileri temsil eden DTO
    public class ImagePredictionRequest
    {
        [Required(ErrorMessage = "Görüntü dosyası gereklidir.")]
        // IFormFile, .NET Core'da dosya yüklemek için kullanılır.
        public IFormFile ImageFile { get; set; } = default!;

        // İsteğe bağlı olarak, eğer mobil uygulama kullanıcı ID'si gibi ekstra bilgi gönderecekse eklenebilir.
        // public string? UserId { get; set; } 
    }
}