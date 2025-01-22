using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Interfaces.Repositories;

public interface IPokemonTypeRepository
{
    Task<PokemonType> Create(PokemonType entity);
    Task<PokemonType?> GetById(int id);
    Task<PokemonType?> GetByName(string name);
    Task<IEnumerable<PokemonType>> All(string? name = null);
    Task<IEnumerable<PokemonType?>> AllById(IEnumerable<int> ids);
    Task Update(PokemonType entity);
    Task DeleteById(int id);
}
