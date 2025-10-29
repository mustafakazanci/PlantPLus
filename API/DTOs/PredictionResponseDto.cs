using PlantDiseaseApi.Models; 

namespace PlantDiseaseApi.DTOs
{
    public class PredictionResponseDto
    {
        public string PredictedDiseaseName { get; set; } = string.Empty;

        public float Confidence { get; set; }

        public string DiseaseDescription { get; set; } = string.Empty;

        public IEnumerable<Solution> SuggestedSolutions { get; set; } = new List<Solution>();
    }
}