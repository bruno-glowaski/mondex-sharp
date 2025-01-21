using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Backend.Dtos;

public record UpsertPokemonSpeciesDto(
  [Range(0, int.MaxValue)] int Number,
  [Required] string Name,
  [Required] string Genera,
  string Description,
  IEnumerable<int> Types,
  PokemonStatsDto BaseStats
)
{
    public async Task<PokemonSpecies> ToDomain(IPokemonTypeRepository typeRepository)
    {
        return PokemonSpecies.Create(
          0,
          Number,
          Name,
          Genera,
          Description,
          await typeRepository.AllById(Types) ?? throw new KeyNotFoundException($"Could not find one or more of the specified types."),
          BaseStats.ToDomain()
        );
    }
}
