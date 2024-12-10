using TheApp.Infrastructure.Persistence;
using TheApp.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheApp.Domain.Interfaces;
using TheApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace TheApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TheAppDbContext>(options => options.UseSqlServer(
                           configuration.GetConnectionString("TheAppCS")));

            services.AddDefaultIdentity<IdentityUser>(options => { options.Stores.MaxLengthForKeys = 450; })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TheAppDbContext>();

            services.AddScoped<AdminSeeder>();

            services.AddScoped<DentalStudioSeeder>();

            services.AddScoped<IDentalStudioRepository, DentalStudioRepository>();
            services.AddScoped<IUserIdentityManagerRepository, UserIdentityManagerRepository>();
            services.AddScoped<IDentalStudioServiceRepository, DentalStudioServiceRepository>();
        }
    }
}
