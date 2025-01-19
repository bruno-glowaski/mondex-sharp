using Microsoft.EntityFrameworkCore;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Contexts;

public class MonDexSharpDbContext(DbContextOptions<MonDexSharpDbContext> options) : DbContext(options)
{
    public DbSet<PokemonSpecies> Species { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<PokemonSpecies>().HasKey(static e => e.Id);
    }
}
