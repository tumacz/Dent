using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TheApp.Application.Mappings;
using FluentValidation.AspNetCore;
using FluentValidation;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using System.Reflection;
using TheApp.Application.ApplicationUser;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace TheApp.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                cfg.AddProfile(new DentalStudioMappingProfile(userContext));
                cfg.AddProfile(new UserDtoMappingProfile(userManager));
            }).CreateMapper()
            );
                
            services.AddValidatorsFromAssemblyContaining<CreateDentalStudioCommandValidator>()
                .AddFluentValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
