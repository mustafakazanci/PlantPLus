using PlantDiseaseApi.DTOs;

namespace PlantDiseaseApi.DTOs
{
    public class DiseaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        // İlişkili Çözüm önerilerini de DTO olarak sunuyoruz
        public IEnumerable<SolutionDto> SuggestedSolutions { get; set; } = new List<SolutionDto>();
    }
}