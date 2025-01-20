using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Contexts;
using MonDexSharp.Backend.Repositories;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MonDexSharpDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainDb")));
builder.Services.AddScoped<IPokemonSpeciesRepository, PokemonSpeciesRepository>();
builder.Services.AddScoped<CreatePokemonSpeciesUseCase>();
builder.Services.AddScoped<UpdatePokemonSpeciesUseCase>();
builder.Services.AddScoped<DeletePokemonSpeciesUseCase>();
builder.Services.AddControllers().AddJsonOptions(static x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
});

WebApplication app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
