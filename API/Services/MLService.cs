using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Data;
using PlantDiseaseApi.DTOs;

namespace PlantDiseaseApi.Services
{
    // Python servisinden gelecek yanıtı temsil eden iç sınıf
    internal class PythonPredictionResponse
    {
        public string? prediction_label { get; set; }

        // Bu attribute, JSON'daki değer tırnak içinde ("0.98") gelse bile
        // onu bir sayıya (float) çevirmeye çalışmasını sağlar.
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public float confidence { get; set; }
    }

    public class MLService : IMLService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MLService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _predictionServiceUrl;

        public MLService(ApplicationDbContext context, ILogger<MLService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _predictionServiceUrl = configuration["PredictionService:Url"] 
                ?? throw new InvalidOperationException("Tahmin servis URL'si (PredictionService:Url) appsettings.json'da yapılandırılmamış.");
        }

        public async Task<PredictionResponseDto> PredictAsync(IFormFile file)
        {
            try
            {
                _logger.LogInformation("Python tahmin servisine istek gönderiliyor: {Url}", _predictionServiceUrl);
                var client = _httpClientFactory.CreateClient();

                using var content = new MultipartFormDataContent();
                using var streamContent = new StreamContent(file.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(streamContent, "file", file.FileName);

                var response = await client.PostAsync(_predictionServiceUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Python servisi başarısız yanıt döndü. Statü: {StatusCode}, Hata: {Error}", response.StatusCode, errorContent);
                    return new PredictionResponseDto { IsSuccess = false, ErrorMessage = "Yapay zeka servisiyle iletişim kurulamadı." };
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var pythonResult = JsonSerializer.Deserialize<PythonPredictionResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (pythonResult == null || string.IsNullOrEmpty(pythonResult.prediction_label))
                {
                    _logger.LogWarning("Python servisinden geçersiz veya boş yanıt alındı: {Response}", jsonResponse);
                    return new PredictionResponseDto { IsSuccess = false, ErrorMessage = "Yapay zeka servisinden geçersiz yanıt alındı." };
                }
                
                _logger.LogInformation("Python servisinden sonuç alındı: Label='{Label}', Güven={Confidence}", pythonResult.prediction_label, pythonResult.confidence);

                var detectedDisease = await _context.Diseases
                    .Include(d => d.DiseaseSolutions)!.ThenInclude(ds => ds.Solution)
                    .FirstOrDefaultAsync(d => d.PredictionLabel == pythonResult.prediction_label);

                if (detectedDisease == null)
                {
                    _logger.LogWarning("Tespit edilen etiket '{Label}' veritabanında bulunamadı.", pythonResult.prediction_label);
                    return new PredictionResponseDto { IsSuccess = false, ErrorMessage = $"Tespit edilen etiket '{pythonResult.prediction_label}' veritabanında bulunamadı." };
                }

                return new PredictionResponseDto
                {
                    IsSuccess = true,
                    PredictedDiseaseName = detectedDisease.Name,
                    Confidence = pythonResult.confidence,
                    DiseaseDescription = detectedDisease.Description,
                    Symptoms = detectedDisease.Symptoms,
                    Cause = detectedDisease.Cause,
                    Prevention = detectedDisease.Prevention,
                    SuggestedSolutions = detectedDisease.DiseaseSolutions!.Select(ds => new SolutionDto
                    {
                        Id = ds.Solution!.Id,
                        Title = ds.Solution.Title,
                        Description = ds.Solution.Description,
                        ReferenceUrl = ds.Solution.ReferenceUrl
                    }).ToList()
                };
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Python servisine bağlanırken bir ağ hatası oluştu. Servis çalışıyor mu? URL: {Url}", _predictionServiceUrl);
                return new PredictionResponseDto { IsSuccess = false, ErrorMessage = "Yapay zeka servisine bağlanılamıyor." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tahmin servisinde beklenmedik bir hata oluştu.");
                return new PredictionResponseDto { IsSuccess = false, ErrorMessage = "Sunucuda beklenmedik bir hata oluştu." };
            }
        }
    }
}