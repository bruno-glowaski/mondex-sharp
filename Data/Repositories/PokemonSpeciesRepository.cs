using Microsoft.EntityFrameworkCore;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Data.Contexts;
using MonDexSharp.Data.Models;

namespace MonDexSharp.Data.Repositories;

public class PokemonSpeciesRepository(MonDexSharpDbContext dbContext) : IPokemonSpeciesRepository
{
    private readonly MonDexSharpDbContext dbContext = dbContext;

    public async Task<PokemonSpecies> Create(PokemonSpecies entity)
    {
        PokemonSpeciesModel model = await ConvertToModel(entity);
        _ = dbContext.Species.Add(model);
        _ = await dbContext.SaveChangesAsync();
        return model.ToDomain();
    }
    public async Task<IEnumerable<PokemonSpecies>> All(string? query = null)
    {
        return (await AllQuery()
              .Where(t =>
                query == null ||
                t.Name.Contains(query!) ||
                t.Genera.Contains(query!) ||
                t.Description.Contains(query!))
              .ToArrayAsync()).Select(static m => m.ToDomain());
    }
    public async Task<bool> AnyWithTypeId(int typeId)
    {
        return await AllQuery().AnyAsync(m => m.Types.Any(m => m.Id == typeId));
    }
    public async Task<PokemonSpecies?> GetById(int id)
    {
        return (await AllQuery().SingleAsync(m => m.Id == id))?.ToDomain();
    }
    public async Task Update(PokemonSpecies entity)
    {
        PokemonSpeciesModel model = await dbContext.Species.FindAsync(entity.Id) ?? throw new KeyNotFoundException();
        await ApplyOntoModel(model, entity);
        _ = await dbContext.SaveChangesAsync();
    }
    public async Task DeleteById(int id)
    {
        _ = await dbContext.Species.Where(m => m.Id == id).ExecuteDeleteAsync();
    }

    private IQueryable<PokemonSpeciesModel> AllQuery()
    {
        return dbContext.Species.Include(static m => m.Types);
    }

    private async Task<PokemonSpeciesModel> ConvertToModel(PokemonSpecies entity)
    {
        PokemonSpeciesModel result = new() { Id = 0 };
        await ApplyOntoModel(result, entity);
        return result;
    }

    private async Task ApplyOntoModel(PokemonSpeciesModel dest, PokemonSpecies src)
    {
        int[] typeIds = [.. src.Types.Select(static t => t.Id!.Value)];
        dest.Number = src.Number;
        dest.Name = src.Name;
        dest.Genera = src.Genera;
        dest.Description = src.Description;
        dest.Types = await dbContext.Types.Where(t => typeIds.Contains(t.Id)).ToArrayAsync();
        dest.BaseStats = new(src.BaseStats);
    }
}
