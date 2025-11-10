using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseApi.Models
{
    public class Solution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty; 

        [Required]
        public string Description { get; set; } = string.Empty;

        public string ReferenceUrl { get; set; } = string.Empty;

        public ICollection<DiseaseSolution>? DiseaseSolutions { get; set; }
    }
}