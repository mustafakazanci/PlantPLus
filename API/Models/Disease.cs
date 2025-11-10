using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseApi.Models
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string PredictionLabel { get; set; } = string.Empty;

        [Required] 
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty; 
        
        public string Symptoms { get; set; } = string.Empty; 
        
        public string Cause { get; set; } = string.Empty;
        
        public string Prevention { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty; 

        public ICollection<DiseaseSolution>? DiseaseSolutions { get; set; }
    }
}