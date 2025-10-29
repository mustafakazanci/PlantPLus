using System.ComponentModel.DataAnnotations;

namespace PlantDiseaseApi.Models
{
    public class Solution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty; // Çözüm önerisinin başlığı (örn: "Organik Sprey Uygulaması")

        [Required]
        public string Description { get; set; } = string.Empty; // Çözüm önerisinin detaylı açıklaması

        public string ReferenceUrl { get; set; } = string.Empty; // Çözüm için harici kaynak (örn: bir makale linki)

        // Bir çözüm önerisi birden fazla hastalığa uygulanabilir.
        public ICollection<DiseaseSolution>? DiseaseSolutions { get; set; }
    }
}