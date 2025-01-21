using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Contexts;
using MonDexSharp.Backend.Repositories;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MonDexSharpDbContext>(options =>
  {
      _ = options
        .UseSqlServer(builder.Configuration.GetConnectionString("MainDb"),
          options => options.EnableRetryOnFailure()
      );
  }
);

builder.Services.AddScoped<IPokemonSpeciesRepository, PokemonSpeciesRepository>();
builder.Services.AddScoped<IPokemonTypeRepository, PokemonTypeRepository>();

builder.Services.AddScoped<CreatePokemonSpeciesUseCase>();
builder.Services.AddScoped<UpdatePokemonSpeciesUseCase>();
builder.Services.AddScoped<DeletePokemonSpeciesUseCase>();

builder.Services.AddCors(options =>
{
    // For simplicity and time saving, we are exposing the backend directly. The correct way would be to make the frontend
    // redirect requests so only the frontend can connect to it.
    options.AddDefaultPolicy(policy => { _ = policy.AllowAnyOrigin(); });
});
builder.Services.ConfigureHttpJsonOptions(static o =>
{
    o.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
});
builder.Services.AddControllers().AddJsonOptions(static x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
});
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    _ = app.MapScalarApiReference();
}
app.MapHealthChecks("health");

if (bool.TryParse(app.Configuration["RunMigrations"], out bool runMigrations) && runMigrations)
{
    using IServiceScope scope = app.Services.CreateScope();

    IServiceProvider services = scope.ServiceProvider;

    try
    {
        MonDexSharpDbContext context = services.GetRequiredService<MonDexSharpDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Microsoft.Data.SqlClient.SqlException)
    {
        app.Logger.LogError("Could not apply migrations. Database might be outdated.");
    }
}

app.Run();
