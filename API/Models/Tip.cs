using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseApi.Models
{
    public class Tip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlantName { get; set; } = string.Empty; 

        [Required]
        [MaxLength(250)]
        public string Content { get; set; } = string.Empty; 
    }
}