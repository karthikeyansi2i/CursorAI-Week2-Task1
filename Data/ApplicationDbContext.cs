using Microsoft.EntityFrameworkCore;
using BMICalculator.API.Models;

namespace BMICalculator.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BmiCategory> BmiCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed BMI categories
            modelBuilder.Entity<BmiCategory>().HasData(
                new BmiCategory { Id = 1, Name = "Underweight", MinValue = 0, MaxValue = 18.5 },
                new BmiCategory { Id = 2, Name = "Normal weight", MinValue = 18.5, MaxValue = 25 },
                new BmiCategory { Id = 3, Name = "Overweight", MinValue = 25, MaxValue = 30 },
                new BmiCategory { Id = 4, Name = "Obese", MinValue = 30, MaxValue = null }
            );
        }
    }
} 