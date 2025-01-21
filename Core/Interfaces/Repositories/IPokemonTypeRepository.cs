using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Interfaces.Repositories;

public interface IPokemonTypeRepository
{
    Task<PokemonType> Create(PokemonType entity);
    Task<PokemonType?> GetById(int id);
    Task<IEnumerable<PokemonType>> All();
    Task<IEnumerable<PokemonType?>> AllById(IEnumerable<int> ids);
}
