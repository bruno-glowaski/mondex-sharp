using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Contexts;
using MonDexSharp.Backend.Repositories;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MonDexSharpDbContext>(options =>
  options
    .UseSqlServer(builder.Configuration.GetConnectionString("MainDb"))
    .EnableSensitiveDataLogging()
);

builder.Services.AddScoped<IPokemonSpeciesRepository, PokemonSpeciesRepository>();
builder.Services.AddScoped<IPokemonTypeRepository, PokemonTypeRepository>();

builder.Services.AddScoped<CreatePokemonSpeciesUseCase>();
builder.Services.AddScoped<UpdatePokemonSpeciesUseCase>();
builder.Services.AddScoped<DeletePokemonSpeciesUseCase>();

builder.Services.ConfigureHttpJsonOptions(static o =>
{
    o.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
});
builder.Services.AddControllers().AddJsonOptions(static x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddOpenApi();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    _ = app.MapScalarApiReference();
}

app.Run();
