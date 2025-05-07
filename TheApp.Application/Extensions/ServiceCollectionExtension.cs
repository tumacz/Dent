using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TheApp.Application.ApplicationUser;
using TheApp.Application.DentalStudios.Commands.CreateDentalStudio;
using TheApp.Application.DentalStudios.Commands.EditDentalStudio;
using TheApp.Application.DentalStudioServiceDTO.Commands.CreateService;
using TheApp.Application.DentalStudioServiceDTO.Commands.EditService;
using TheApp.Application.Mappings;

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
                cfg.AddProfile(new ServiceMappingProfile());
            }).CreateMapper()
            );

            //DentalStudio
            services.AddValidatorsFromAssemblyContaining<CreateDentalStudioCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<EditDentalStudioCommandValidator>();
            //Service
            services.AddValidatorsFromAssemblyContaining<CreateDentalStudioServiceCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<EditDentalStudioServiceCommandValidator>();
            //User
            services.AddValidatorsFromAssemblyContaining<EditUserCommandValidator>();
            //Appointment
            services.AddValidatorsFromAssemblyContaining<CreateAppointmentCommandValidator>();

            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
        }
    }
}