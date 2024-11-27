using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheApp.Domain.Entities;

namespace TheApp.Infrastructure.Persistence
{
    public class TheAppDbContext : IdentityDbContext
    {
        public TheAppDbContext(DbContextOptions<TheAppDbContext> options) : base(options) { }

        public DbSet<DentalStudio> DentalStudios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DentalStudio>()
                .OwnsOne(c => c.ContactDetails);
        }
    }
}
