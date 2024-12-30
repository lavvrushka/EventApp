using EventApp.Application.Common.Validation.Validators.Event;
using EventApp.Application.Common.Validation.Validators.Location;
using EventApp.Application.Common.Validation.Validators.User;
using FluentValidation;

namespace EventApp.API.Extensions
{
    public static class ValidationServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateEventRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateEventRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LocationRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LocationResponseValidator>();
            services.AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();

            return services;
        }
    }
}
