using Microsoft.EntityFrameworkCore;
using AirlineSimulation.Infrastructure.Data;
using AirlineSimulation.Application.Interfaces;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<IValidationService, AirlineSimulation.Infrastructure.Services.ValidationService>();
builder.Services.AddScoped<IBaggageTagService, AirlineSimulation.Infrastructure.Services.BaggageTagService>();
builder.Services.AddScoped<ITicketService, AirlineSimulation.Infrastructure.Services.TicketService>();
builder.Services.AddScoped<IFlightService, AirlineSimulation.Infrastructure.Services.FlightService>();
builder.Services.AddScoped<IPassengerService, AirlineSimulation.Infrastructure.Services.PassengerService>();

// Add CORS - Specific for Netlify
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Controllers
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AirlineDbContext>();
    // context.Database.Migrate(); // Disabled auto-migration to prevent conflicts
    DbSeeder.SeedData(context); // Seed airline data
    await CustomsDataSeeder.SeedCustomsDataAsync(context); // Seed customs data

    // Clean up misspelled terminal locations
    try
    {
        await context.Database.ExecuteSqlRawAsync("UPDATE BaggageTags SET CurrentLocation = 'Terminal' WHERE CurrentLocation = 'Termnal'");
        await context.Database.ExecuteSqlRawAsync("UPDATE BaggageTags SET PreviousLocation = 'Terminal' WHERE PreviousLocation = 'Termnal'");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error correcting 'Termnal' spelling: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(); // Scalar UI at /scalar

// app.UseHttpsRedirection(); // Disable to avoid SSL issues on free tier
app.UseCors("AllowAll"); // Before Authorization



// Serve static files (React Frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// Fallback to index.html for React Router (SPA)
app.MapFallbackToFile("index.html");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
