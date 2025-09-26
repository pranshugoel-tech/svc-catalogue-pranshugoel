using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Service.Catalogue.Api.DTOs;

namespace Service.Catalogue.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ServiceCatalogueDto> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some example services
            modelBuilder.Entity<ServiceCatalogueDto>().HasData(
                new ServiceCatalogueDto
                {
                    Id = Guid.NewGuid(),
                    Name = "User Management",
                    OwnerTeam = "Identity",
                    Tier = "gold",
                    Lifecycle = "production",
                    Endpoints = new List<string> { "https://api.example.com/users" },
                    Tags = new List<string> { "auth", "users" },
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ServiceCatalogueDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Payments API",
                    OwnerTeam = "Finance",
                    Tier = "silver",
                    Lifecycle = "preprod",
                    Endpoints = new List<string> { "https://api.example.com/payments" },
                    Tags = new List<string> { "payments", "finance" },
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
