using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Models; 

namespace PlantDiseaseApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<DiseaseSolution> DiseaseSolutions { get; set; }

        // Many-to-Many ilişkiyi yapılandırmak için OnModelCreating metodunu override ediyoruz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<DiseaseSolution>()
                .HasKey(ds => new { ds.DiseaseId, ds.SolutionId });

            modelBuilder.Entity<DiseaseSolution>()
                .HasOne(ds => ds.Disease)
                .WithMany(d => d.DiseaseSolutions)
                .HasForeignKey(ds => ds.DiseaseId);

            modelBuilder.Entity<DiseaseSolution>()
                .HasOne(ds => ds.Solution)
                .WithMany(s => s.DiseaseSolutions)
                .HasForeignKey(ds => ds.SolutionId);

            base.OnModelCreating(modelBuilder); 
        }
    }
}