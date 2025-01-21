using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Dtos;

public record PokemonTypeDto(int Id, string Name) : UpsertPokemonTypeDto(Name)
{
    public PokemonTypeDto(PokemonType entity) : this(entity.Id, entity.Name) { }
}
