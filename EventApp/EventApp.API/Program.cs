using EventApp.API.Extensions;
using EventApp.API.Middlewares;
using EventApp.Application.Common.Mappings;
using EventApp.Application.Common.Validation;
using EventApp.Domain.Interfaces.IServices;
using EventApp.Infrastructure.Persistence.Context;
using EventApp.Infrastructure.Services;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(EventProfile).Assembly);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddValidationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomMiddlewares();
builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationService<,>));

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите 'Bearer' [пробел] и ваш токен. Пример: 'Bearer abcdef12345'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins("http://localhost:3000") 
                        .AllowAnyMethod()  
                        .AllowAnyHeader()  
                        .AllowCredentials());
});

var app = builder.Build();
app.UseCors("AllowReact"); 
app.UseMiddleware<ValidationMiddleware>();  
app.UseMiddleware<GlobalExceptionMiddleware>();  
await ApplicationDbContextInitializer.InitializeAsync(app.Services);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();  
app.UseAuthentication();  
app.UseAuthorization();   
app.MapControllers();  
app.Run();
