using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Contexts;
using MonDexSharp.Backend.Models;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Backend.Repositories;

public class PokemonSpeciesRepository(MonDexSharpDbContext dbContext) : IPokemonSpeciesRepository
{
    private readonly MonDexSharpDbContext dbContext = dbContext;

    public async Task Create(PokemonSpecies entity)
    {
        _ = dbContext.Species.Add(new(entity));
        _ = await dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<PokemonSpecies>> All()
    {
        return await dbContext.Species.Select(static m => m.ToDomain()).ToListAsync();
    }
    public async Task<PokemonSpecies?> GetById(int id)
    {
        return (await dbContext.Species.FindAsync(id))?.ToDomain();
    }
    public async Task Update(PokemonSpecies entity)
    {
        PokemonSpeciesModel model = await dbContext.Species.FindAsync(entity.Id) ?? throw new KeyNotFoundException();
        model.Name = entity.Name;
        model.BaseStats = new(entity.BaseStats);
        _ = await dbContext.SaveChangesAsync();
    }
    public async Task DeleteById(int id)
    {
        _ = await dbContext.Species.Where(m => m.Id == id).ExecuteDeleteAsync();
    }
}
