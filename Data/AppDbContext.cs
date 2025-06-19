using Microsoft.EntityFrameworkCore;
using SpilAPI.Models;

namespace SpilAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Bruger> Brugere { get; set; }
        public DbSet<Spil> Spil { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure Username is unique
            modelBuilder.Entity<Bruger>()
                .HasIndex(u => u.Brugernavn)
                .IsUnique();

            // Seed initial games
            modelBuilder.Entity<Spil>().HasData(
                new Spil
                {
                    SpilId = 1,
                    Navn = "Boldspil"
                   
                },
                new Spil
                {
                    SpilId = 2,
                    Navn = "Puslespil"
                   
                }
            );
        }
    }
}
