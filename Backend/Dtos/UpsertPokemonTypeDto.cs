using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Dtos;

public record UpsertPokemonTypeDto([Required] string Name)
{
    public PokemonType ToDomain(int? withId = null)
    {
        return PokemonType.Create(withId ?? 0, Name);
    }
}
