using EventApp.Application.DTOs.User.Requests;
using EventApp.Application.UseCases.AuthUsecases;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Application.UseCases.UserUsecases;
using EventShowcase.Application.UseCases.UserUseCases;

namespace EventApp.API.Extensions
{
    public static class ApplicationCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(UserLoginRequest).Assembly);
            });

            
            services.AddScoped<LoginHandler>();
            services.AddScoped<LogoutHandler>();
            services.AddScoped<RefreshTokenHandler>();
            services.AddScoped<RegisterUserHandler>();

            
            services.AddScoped<AddEventHandler>();
            services.AddScoped<DeleteEventHandler>();
            services.AddScoped<GetAllEventsHandler>();
            services.AddScoped<GetEventByDateHandler>();
            services.AddScoped<GetEventByPageHandler>();
            services.AddScoped<GetEventByIdHandler>();
            services.AddScoped<GetEventByTitleHandler>();
            services.AddScoped<GetFilterEventHandler>();
            services.AddScoped<UpdateEventHandler>();
            services.AddScoped<GetEventsSortedByCategoryHandler>();
            services.AddScoped<GetEventsSortedByLocationHandler>();

            
            services.AddScoped<DeleteUserInEventHandler>();
            services.AddScoped<GetUserEventHandler>();
            services.AddScoped<GetUsersByEventHandler>();
            services.AddScoped<GetUsersByPageHandler>();
            services.AddScoped<RegisterUserToEventHandler>();


            return services;
        }
    }
}
