using TheApp.Infrastructure.Persistence;
using TheApp.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheApp.Domain.Interfaces;
using TheApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using TheApp.Domain.Entities;

namespace TheApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TheAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TheAppCS")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Stores.MaxLengthForKeys = 450;
            })
            .AddEntityFrameworkStores<TheAppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<AdminSeeder>();
            services.AddScoped<DentalStudioSeeder>();

            services.AddScoped<IDentalStudioRepository, DentalStudioRepository>();
            services.AddScoped<IDentalStudioServiceRepository, DentalStudioServiceRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IServiceTagRepository, ServiceTagRepository>();
        }
    }
}