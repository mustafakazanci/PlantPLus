using PlantDiseaseApi.DTOs;

namespace PlantDiseaseApi.Services
{
    // ML Servisi arayüzü, bağımlılık enjeksiyonu için
    public interface IMLService
    {
        // Tahmin işlemini yapacak asenkron metot
        Task<PredictionResponseDto> PredictDiseaseAsync(IFormFile imageFile);
    }
}