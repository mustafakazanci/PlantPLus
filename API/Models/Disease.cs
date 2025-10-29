using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseApi.Models
{
    public class Disease
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        [MaxLength(100)] // Maksimum karakter uzunluğunu belirler.
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty; 
        public string Symptoms { get; set; } = string.Empty; 
        public string ImageUrl { get; set; } = string.Empty; 

        // Bir hastalığın birden fazla çözüm önerisi olabilir (Many-to-Many veya One-to-Many ilişki)
        public ICollection<DiseaseSolution>? DiseaseSolutions { get; set; }
    }
}