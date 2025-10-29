// MLService.cs (Services klasöründe)

// Gerekli kütüphaneler (using'ler) yukarıda eklenmiş olmalı:
using PlantDiseaseApi.DTOs;
using Microsoft.AspNetCore.Http;
using PlantDiseaseApi.Data;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using PlantDiseaseApi.Models;

namespace PlantDiseaseApi.Services
{
    public class MLService : IMLService
    {
        private readonly ApplicationDbContext _context;

        public MLService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PredictionResponseDto> PredictDiseaseAsync(IFormFile imageFile)
        {
            // --- Görüntü Ön İşleme Kısmı (GÜN 4'ten) ---
            byte[] preprocessedImageBytes;
            const int targetWidth = 224;
            const int targetHeight = 224;

            using (var imageStream = imageFile.OpenReadStream())
            {
                // Görüntüyü yükle, yeniden boyutlandır, byte dizisine dönüştür
                using var image = await Image.LoadAsync<Rgb24>(imageStream);
                
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(targetWidth, targetHeight),
                    Mode = ResizeMode.Crop
                }));

                using var memoryStream = new MemoryStream();
                await image.SaveAsJpegAsync(memoryStream); 
                preprocessedImageBytes = memoryStream.ToArray();
            }

            // --- GÜN 6, Adım 4: İyileştirilmiş Sahte Tahmin Mantığı ---
            
            // Veritabanındaki Pas Hastalığı (ID 1) ve Külleme Hastalığı (ID 2) arasında rastgele seçim.
            // .Next(1, 3) komutu, 1 veya 2 (3 hariç) üretir.
            Random random = new Random();
            int predictedClassId = random.Next(1, 3); 
            
            // %70 ile %99 arası rastgele güven skoru üretir.
            float confidenceScore = (float)random.NextDouble() * (0.99f - 0.70f) + 0.70f; 

            // Veritabanından tahmini hastalığı bul ve ilişkili çözümleri getir
            var disease = await _context.Diseases
                .Include(d => d.DiseaseSolutions)!
                .ThenInclude(ds => ds.Solution)
                .FirstOrDefaultAsync(d => d.Id == predictedClassId);

            if (disease == null)
            {
                // Eğer sahte ID bile veritabanında yoksa (ki olmamalı)
                return new PredictionResponseDto 
                { 
                    PredictedDiseaseName = "Sistem Hatası", 
                    Confidence = 0.0f,
                    DiseaseDescription = "Sahte tahmin ID'si veritabanında bulunamadı."
                };
            }

            // Response DTO'yu hazırlıyoruz
            var response = new PredictionResponseDto
            {
                PredictedDiseaseName = disease.Name,
                Confidence = confidenceScore,
                DiseaseDescription = disease.Description,
                // İlişkili çözüm önerilerini DTO'ya dönüştürüyoruz (GÜN 5'ten Model kullanıyorduk, DTO'ya dönüştürülmeli)
                SuggestedSolutions = (IEnumerable<Solution>)(disease.DiseaseSolutions?.Select(ds => new DTOs.SolutionDto // DTO'ya dönüştürmek için düzeltme
                {
                    Id = ds.Solution!.Id,
                    Title = ds.Solution.Title,
                    Description = ds.Solution.Description,
                    ReferenceUrl = ds.Solution.ReferenceUrl
                }) ?? Enumerable.Empty<DTOs.SolutionDto>())
            };

            return response;
        }
    }
}