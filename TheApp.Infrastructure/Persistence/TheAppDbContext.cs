using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;

namespace TheApp.Infrastructure.Persistence
{
    public class TheAppDbContext : IdentityDbContext
    {
        public TheAppDbContext(DbContextOptions<TheAppDbContext> options) : base(options) { }

        public DbSet<DentalStudio> DentalStudios { get; set; }
        public DbSet<DentalStudioService> Sevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DentalStudio>()
                .OwnsOne(c => c.ContactDetails);
            modelBuilder.Entity<DentalStudio>()
                .HasMany(c => c.Sevices)
                .WithOne(c => c.DentalStudio)
                .HasForeignKey(c => c.DentalStudioId);
        }
    }
}