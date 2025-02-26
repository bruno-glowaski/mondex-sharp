using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Dtos;

public record PokemonSpeciesDto(int Id,
  int Number,
  string Name,
  string Genera,
  string Description,
  PokemonTypeDto[] Types,
  PokemonStatsDto BaseStats
)
{
    public PokemonSpeciesDto(PokemonSpecies entity) : this(entity.Id!.Value, entity.Number, entity.Name, entity.Genera, entity.Description, [.. entity.Types.Select(static t => new PokemonTypeDto(t))], new(entity.BaseStats)) { }
}
