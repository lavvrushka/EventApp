using EventApp.Application.Common.Mappings;

namespace EventApp.API.Extensions
{
    public static class MappingServiceCollectionExtensions
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EventProfile).Assembly);
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddAutoMapper(typeof(LocationProfile).Assembly);
            services.AddAutoMapper(typeof(EventProfile).Assembly);

            return services;
        }
    }
}
