using Microsoft.EntityFrameworkCore;
using MonDexSharp.Data.Models;

namespace MonDexSharp.Data.Contexts;

public class MonDexSharpDbContext(DbContextOptions<MonDexSharpDbContext> options) : DbContext(options)
{
    public DbSet<PokemonSpeciesModel> Species { get; set; } = null!;
    public DbSet<PokemonTypeModel> Types { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<PokemonTypeModel>(static b =>
        {
            _ = b.HasKey(static m => m.Id);
            _ = b.HasMany(static m => m.Species).WithMany(static m => m.Types);
        });
        _ = modelBuilder.Entity<PokemonSpeciesModel>(static b =>
        {
            _ = b.HasKey(static m => m.Id);
            _ = b.ComplexProperty(static m => m.BaseStats);
            _ = b.HasMany(static m => m.Types).WithMany(static m => m.Species);
        });
    }
}
