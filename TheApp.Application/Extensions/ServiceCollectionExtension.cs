using Microsoft.Extensions.DependencyInjection;
using TheApp.Application.Mappings;
using FluentValidation.AspNetCore;
using FluentValidation;
using TheApp.Application.DataTransferObjects.Commands.CreateDentalStudio;
using System.Reflection;
using TheApp.Application.ApplicationUser;
using AutoMapper;
using TheApp.Application.ApplicationUser.UserDTO.Commands;

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
                cfg.AddProfile(new DentalStudioMappingProfile(userContext));
                cfg.AddProfile(new AppUserMappingProfile());
                cfg.AddProfile(new AppointmentMappingProfile());
            }).CreateMapper()
            );
                
            services.AddValidatorsFromAssemblyContaining<CreateDentalStudioCommandValidator>()
                .AddFluentValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<EditUserCommandValidator>()
                .AddFluentValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
