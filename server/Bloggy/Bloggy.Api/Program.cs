using Bloggy.Application.Persistense;
using Bloggy.Domain.Entites;
using Bloggy.Infrastructure;
using Bloggy.Infrastructure.Persistense;
using Bloggy.Infrastructure.Persistense.Seed;
using Bloggy.Infrastructure.Services.MeiliSearch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Bloggy",
                        Version = "v1"
                    }
                );

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                var securityRequirement = new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
                };

                options.AddSecurityRequirement(securityRequirement);
            });

            builder.Services.AddSingleton(serviceProvider =>
            {
                var configuration = builder.Configuration;
                // var meiliSearchUrl = configuration["MeiliSearch:Url"];
                // var apiKey = configuration["MeiliSearch:ApiKey"];

                // var meiliSearchUrl = configuration["MeiliSearch:Url"]
                // ?? throw new InvalidOperationException("MeiliSearch:Url is not configured");
                // var apiKey = configuration["MeiliSearch:ApiKey"]
                // ?? throw new InvalidOperationException("MeiliSearch:ApiKey is not configured");
                var meiliSearchUrl = configuration["MEILI_URL"] 
                    ?? configuration["MeiliSearch:Url"]
                    ?? throw new InvalidOperationException("MeiliSearch:Url is not configured");
                var apiKey = configuration["MEILI_API_KEY"] 
                    ?? configuration["MeiliSearch:ApiKey"]
                    ?? throw new InvalidOperationException("MeiliSearch:ApiKey is not configured");
                var searchAPI = configuration["MEILI_SEARCH_API"]
                    ?? configuration["MeiliSearch:SearchAPI"]
                    ?? throw new InvalidOperationException("MeiliSearch:SearchAPI is not configured");
                return new MeiliSearchService(meiliSearchUrl, apiKey, searchAPI); // Pass configuration into the service
            });

            builder.Services.AddScoped<IMeiliSyncService, MeiliSyncService>();

            builder.Services.AddControllers();

            builder.Services.AddHttpContextAccessor();
            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);
        }
        var app = builder.Build();
        {
            app.UseExceptionHandler("/error");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
                        
            var allowedOrigin = builder.Configuration["VITE_FRONTEND_URL"] ?? "http://localhost:5173";  // Default for local

            app.UseCors(options =>
                options.WithOrigins(allowedOrigin)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }

        var meiliSearchService = app.Services.GetRequiredService<MeiliSearchService>();

        try
        {
            await meiliSearchService.TestConnection();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"MeiliSearch connection failed: {ex.Message}");
            // Optionally, you can exit the application if the connection fails
            Environment.Exit(1);  // Exit with an error code (optional)
        }

        // Check for --seed CLI argument to trigger database seeding
        if (args.Contains("--seed"))
        {
            Console.WriteLine("Seeding database...");
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                // // Apply migrations
                // await context.Database.MigrateAsync();
                
                // Seed the database
                await DbSeeder.SeedAsync(context);
            }
            Console.WriteLine("Database seeding completed.");
            return;
        }

        app.Run();
    }
}
