using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Added DataContext created in the Persistence project.
builder.Services.AddDbContext<DataContext>(opt => {
    // Using SQLite as the default database
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add CORS policy to allow local frontend to connect 
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        // Allowing the client to access the backend. The client lives on localhost:3000
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
    });
});

// The Mediator services
// it needs to know where the mediators/handlers are located.
builder.Services.AddMediatR(typeof(Application.Activities.List.Handler));

var app = builder.Build();

// Middleware : is a pipeline so the order of operations matters here
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Needs to match the cors policy defined in the services section.
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// This using statement means that as soon as the scope is finished being used, it will be disposed of.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
    throw;
}

app.Run();
