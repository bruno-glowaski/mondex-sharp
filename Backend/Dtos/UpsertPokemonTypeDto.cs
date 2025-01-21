using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Dtos;

public record UpsertPokemonTypeDto([Required] string Name)
{
    public PokemonType ToDomain()
    {
        return PokemonType.Create(0, Name);
    }
}
