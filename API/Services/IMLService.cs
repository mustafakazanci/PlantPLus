using Microsoft.AspNetCore.Http; 
using PlantDiseaseApi.DTOs;

namespace PlantDiseaseApi.Services
{
    public interface IMLService
    {
        Task<PredictionResponseDto> PredictAsync(IFormFile file);
    }
}