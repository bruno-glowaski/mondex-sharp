using Microsoft.EntityFrameworkCore;
using MonDexSharp.Backend.Contexts;
using MonDexSharp.Backend.Models;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Backend.Repositories;

public class PokemonTypeRepository(MonDexSharpDbContext dbContext) : IPokemonTypeRepository
{
    private readonly MonDexSharpDbContext dbContext = dbContext;

    public async Task<PokemonType> Create(PokemonType entity)
    {
        PokemonTypeModel model = new(entity);
        _ = dbContext.Types.Add(model);
        _ = await dbContext.SaveChangesAsync();
        return model.ToDomain();
    }
    public async Task<PokemonType?> GetById(int id)
    {
        return (await dbContext.Types.FindAsync(id))?.ToDomain();
    }
    public async Task<IEnumerable<PokemonType>> All(string? name = null)
    {
        return await dbContext.Types.Where(t => name == null || t.Name.Contains(name!)).Select(static m => m.ToDomain()).ToListAsync();
    }
    public async Task<IEnumerable<PokemonType?>> AllById(IEnumerable<int> ids)
    {
        IQueryable<PokemonTypeModel> query = dbContext.Types.Where(t => ids.Contains(t.Id));
        PokemonType[] results = await query.Select(static m => m.ToDomain()).ToArrayAsync();
        return ids.Select(id => results.FirstOrDefault(e => e.Id == id));
    }
    public async Task Update(PokemonType entity)
    {
        PokemonTypeModel model = await dbContext.Types.FindAsync(entity.Id) ?? throw new KeyNotFoundException();
        ApplyOntoModel(model, entity);
        _ = await dbContext.SaveChangesAsync();
    }
    public async Task DeleteById(int id)
    {
        _ = await dbContext.Types.Where(m => m.Id == id).ExecuteDeleteAsync();
    }

    private static void ApplyOntoModel(PokemonTypeModel dest, PokemonType src)
    {
        dest.Name = src.Name;
    }
}
