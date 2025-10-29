using System.ComponentModel.DataAnnotations;
using PlantDiseaseApi.Models;

namespace PlantDiseaseApi.Models
{
   
    public class DiseaseSolution
    {
        public int DiseaseId { get; set; }
        public Disease? Disease { get; set; } 

        public int SolutionId { get; set; }
        public Solution? Solution { get; set; }
    }
}