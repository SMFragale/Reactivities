using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Extension method that contains all external services. It exists so that the Program.cs doesnt get too dirty.
builder.Services.AddApplicationServices(builder.Configuration);

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
