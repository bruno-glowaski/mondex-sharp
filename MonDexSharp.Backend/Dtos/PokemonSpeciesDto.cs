using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Dtos;

public record PokemonSpeciesDto(int Id, [Required][MinLength(1)] string Name, PokemonStatsDto BaseStats)
{
    public PokemonSpeciesDto(PokemonSpecies entity) : this(entity.Id, entity.Name, new(entity.BaseStats)) { }

    public PokemonSpecies ToEntity()
    {
        return new(Id, Name, BaseStats.ToValueObject());
    }
}
