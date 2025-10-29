namespace PlantDiseaseApi.DTOs
{
    public class SolutionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReferenceUrl { get; set; } = string.Empty;
    }
}