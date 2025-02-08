using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;

namespace TheApp.Infrastructure.Persistence
{
    public class TheAppDbContext : IdentityDbContext
    {
        public TheAppDbContext(DbContextOptions<TheAppDbContext> options) : base(options) { }

        public DbSet<DentalStudio> DentalStudios { get; set; }
        public DbSet<DentalStudioService> DentalStudioServices { get; set; }
        public DbSet<DentalService> DentalServices {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DentalStudio>()
                .OwnsOne(c => c.ContactDetails);

            modelBuilder.Entity<DentalStudio>()
                .HasMany(c => c.DentalStudioServices)
                .WithOne(c => c.DentalStudio)
                .HasForeignKey(c => c.DentalStudioId);

            modelBuilder.Entity<DentalStudioService>()
                .HasOne(c => c.DentalService)
                .WithMany()
                .HasForeignKey(c => c.DentalServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DentalStudioService>()
                .HasIndex(c => c.DentalServiceId);
        }
    }
}
