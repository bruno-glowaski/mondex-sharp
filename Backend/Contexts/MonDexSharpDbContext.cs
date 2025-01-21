using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Models;

namespace MonDexSharp.Backend.Contexts;

public class MonDexSharpDbContext(DbContextOptions<MonDexSharpDbContext> options) : DbContext(options)
{
    public DbSet<PokemonSpeciesModel> Species { get; set; } = null!;
    public DbSet<PokemonTypeModel> Types { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<PokemonSpeciesModel>(static b =>
        {
            _ = b.ComplexProperty(static e => e.BaseStats);
            _ = b.HasKey(static e => e.Id);
        });
    }
}
