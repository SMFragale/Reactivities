using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    //Contains all services for the application
    public static class ApplicationServiceExtensions
    {
        // This is an extension method. It extends the IServiceCollection interface.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) 
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Added DataContext created in the Persistence project.
            services.AddDbContext<DataContext>(opt =>
            {
                // Using SQLite as the default database
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });


            // Add CORS policy to allow local frontend to connect 
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    // Allowing the client to access the backend. The client lives on localhost:3000
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });

            // The Mediator services
            // it needs to know where the mediators/handlers are located.
            services.AddMediatR(typeof(Application.Activities.List.Handler));

            // The AutoMaper Service
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}