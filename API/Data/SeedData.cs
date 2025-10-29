using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Models;

namespace PlantDiseaseApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Diseases.Any())
                {
                    return; // DB zaten seed edilmiş
                }

                var diseases = new Disease[]
                {
                    new Disease
                    {
                        Name = "Pas Hastalığı (Rust)",
                        Description = "Yapraklarda pas rengi lekelerle karakterize edilen yaygın bir mantar hastalığıdır.",
                        Symptoms = "Yaprakların üst ve alt yüzeylerinde turuncu-kahverengi püstüller, yaprak dökülmesi.",
                        ImageUrl = "https://example.com/rust_disease.jpg" // Gerçek bir URL ile değiştir
                    },
                    new Disease
                    {
                        Name = "Külleme Hastalığı (Powdery Mildew)",
                        Description = "Yapraklar ve gövdeler üzerinde beyaz, pudramsı bir tabaka oluşturan mantar hastalığı.",
                        Symptoms = "Yapraklar, gövdeler ve çiçekler üzerinde beyaz, un gibi lekeler, yaprak kıvrılması.",
                        ImageUrl = "https://example.com/powdery_mildew.jpg" // Gerçek bir URL ile değiştir
                    }
                };
                context.Diseases.AddRange(diseases);
                context.SaveChanges();

                // Örnek Çözümler
                var solutions = new Solution[]
                {
                    new Solution
                    {
                        Title = "Organik Mantar İlacı Uygulaması",
                        Description = "Neem yağı veya potasyum bikarbonat içeren organik mantar ilaçları düzenli olarak uygulanmalıdır.",
                        ReferenceUrl = "https://example.com/organic_fungicide_guide"
                    },
                    new Solution
                    {
                        Title = "Hava Akışını İyileştirme",
                        Description = "Bitkiler arasındaki mesafeyi artırarak veya budama yaparak hava sirkülasyonunu sağlayın.",
                        ReferenceUrl = "https://example.com/plant_spacing_tips"
                    },
                    new Solution
                    {
                        Title = "Nem Kontrolü",
                        Description = "Yaprakları geceleri sulamaktan kaçının ve bitkileri sabah erken saatlerde sulayarak kurumalarına izin verin.",
                        ReferenceUrl = "https://example.com/humidity_control"
                    }
                };
                context.Solutions.AddRange(solutions);
                context.SaveChanges();

                // Hastalık ve Çözüm İlişkilerini Kurma (DiseaseSolution)
                // Örnek: Pas hastalığına hem organik ilaç hem de hava akışı çözümü
                var rustDisease = context.Diseases.FirstOrDefault(d => d.Name == "Pas Hastalığı (Rust)");
                var organicFungicide = context.Solutions.FirstOrDefault(s => s.Title == "Organik Mantar İlacı Uygulaması");
                var airFlowSolution = context.Solutions.FirstOrDefault(s => s.Title == "Hava Akışını İyileştirme");

                if (rustDisease != null && organicFungicide != null)
                {
                    context.DiseaseSolutions.Add(new DiseaseSolution { DiseaseId = rustDisease.Id, SolutionId = organicFungicide.Id });
                }
                if (rustDisease != null && airFlowSolution != null)
                {
                    context.DiseaseSolutions.Add(new DiseaseSolution { DiseaseId = rustDisease.Id, SolutionId = airFlowSolution.Id });
                }

                // Örnek: Külleme hastalığına organik ilaç, hava akışı ve nem kontrolü çözümü
                var powderyMildewDisease = context.Diseases.FirstOrDefault(d => d.Name == "Külleme Hastalığı (Powdery Mildew)");
                var humidityControl = context.Solutions.FirstOrDefault(s => s.Title == "Nem Kontrolü");

                if (powderyMildewDisease != null && organicFungicide != null)
                {
                    context.DiseaseSolutions.Add(new DiseaseSolution { DiseaseId = powderyMildewDisease.Id, SolutionId = organicFungicide.Id });
                }
                if (powderyMildewDisease != null && airFlowSolution != null)
                {
                    context.DiseaseSolutions.Add(new DiseaseSolution { DiseaseId = powderyMildewDisease.Id, SolutionId = airFlowSolution.Id });
                }
                if (powderyMildewDisease != null && humidityControl != null)
                {
                    context.DiseaseSolutions.Add(new DiseaseSolution { DiseaseId = powderyMildewDisease.Id, SolutionId = humidityControl.Id });
                }

                context.SaveChanges();
            }
        }
    }
}