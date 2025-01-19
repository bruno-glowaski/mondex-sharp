using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Contexts;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Backend.Repositories;

public class PokemonSpeciesRepository(MonDexSharpDbContext dbContext) : IPokemonSpeciesRepository
{
    private readonly MonDexSharpDbContext dbContext = dbContext;

    public async Task Create(PokemonSpecies entity)
    {
        _ = dbContext.Species.Add(entity);
        _ = await dbContext.SaveChangesAsync();
    }
    public async Task Update(PokemonSpecies entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        _ = await dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<PokemonSpecies>> All()
    {
        return await dbContext.Species.ToListAsync();
    }
    public async Task<PokemonSpecies?> GetById(int id)
    {
        return await dbContext.Species.FindAsync(id);
    }
}
