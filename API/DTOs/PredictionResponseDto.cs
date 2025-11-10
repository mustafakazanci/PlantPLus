namespace PlantDiseaseApi.DTOs
{
    public class PredictionResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? PredictedDiseaseName { get; set; }
        public float Confidence { get; set; }
        public string? DiseaseDescription { get; set; }
        public string? Symptoms { get; set; }
        public string? Cause { get; set; }
        public string? Prevention { get; set; }
        public IEnumerable<SolutionDto> SuggestedSolutions { get; set; } = new List<SolutionDto>();
        public string? ErrorMessage { get; set; }
    }
}