using Microsoft.EntityFrameworkCore;
using TeslaACDC.API.Services;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Business.Services;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using TeslaACDC.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NikolaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NikolaDatabase")));


//Tarea : mover la conexion de base de datos al pipeline



// Inyeccion de dependencias
builder.Services.AddScoped<IAlbumRepository<int, Album>, AlbumRepository<int, Album>>();
builder.Services.AddScoped<IAlbumService>(provider =>
{
    var albumRepo = provider.GetRequiredService<IAlbumRepository<int, Album>>();
    return new AlbumService(albumRepo);
});

builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<IMatematika, Matematika>();



// Concepto ___ Despues lo veremos
// builder.Services.AddSingleton<>();
// builder.Services.AddTransient<>

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

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
