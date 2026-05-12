using GymSystem.Configuration;
using GymSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Contexts
{
    public class GymDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;database=GymDb;trusted_Connection=true;trustServerCertificate=true"); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>(new PlanConfiguration());
        }
        public DbSet<Plan> Plans { get; set; }
    }
}
