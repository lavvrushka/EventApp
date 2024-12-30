using EventApp.API.Middlewares;

namespace EventApp.API.Extensions
{
    public static class MiddlewareServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionMiddleware>();
            return services;
        }
    }
}
