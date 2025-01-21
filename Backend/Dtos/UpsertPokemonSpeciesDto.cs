using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Backend.Dtos;

public record UpsertPokemonSpeciesDto(
  [Range(0, int.MaxValue)] int Number,
  [Required] string Name,
  [Required] string Genera,
  string Description,
  int[] Types,
  PokemonStatsDto BaseStats
)
{
    public async Task<PokemonSpecies> ToDomain(IPokemonTypeRepository typeRepository, int? withId = null)
    {
        IEnumerable<PokemonType> types = (await typeRepository.AllById(Types)).Index()
          .Select((t) => t.Item ?? throw new KeyNotFoundException($"Could not find type with id '{Types[t.Index]}'"));
        return PokemonSpecies.Create(
          withId,
          Number,
          Name,
          Genera,
          Description,
          types,
          BaseStats.ToDomain()
        );
    }
}
