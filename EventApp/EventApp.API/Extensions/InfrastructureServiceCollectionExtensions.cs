﻿using EventApp.Application.Common.Interfaces.IRepositories;
using EventApp.Application.Common.Interfaces.IServices;
using EventApp.Application.Common.Interfaces;
using EventApp.Infrastructure.Persistence.Context;
using EventApp.Infrastructure.Persistence.Repositories;
using EventApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using EventApp.Infrastructure.Persistence;

namespace EventApp.API.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IHashPassword, HashPassword>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}