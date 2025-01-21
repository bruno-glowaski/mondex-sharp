using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Dtos;

public record PokemonSpeciesDto(int Id,
  int Number,
  string Name,
  string Genera,
  string Description,
  int[] Types,
  PokemonStatsDto BaseStats
) : UpsertPokemonSpeciesDto(Number, Name, Genera, Description, Types, BaseStats)
{
    public PokemonSpeciesDto(PokemonSpecies entity) : this(entity.Id!.Value, entity.Number, entity.Name, entity.Genera, entity.Description, [.. entity.Types.Select(static t => t.Id!.Value)], new(entity.BaseStats)) { }
}
