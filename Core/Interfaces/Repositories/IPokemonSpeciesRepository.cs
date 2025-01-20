using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Interfaces.Repositories;

public interface IPokemonSpeciesRepository
{
    Task Create(PokemonSpecies entity);
    Task Update(PokemonSpecies entity);
    Task<IEnumerable<PokemonSpecies>> All();
    Task<PokemonSpecies?> GetById(int id);
    Task DeleteById(int id);
}
