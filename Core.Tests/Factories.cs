using MonDexSharp.Core.Entities;
using MonDexSharp.Core.ValueObjects;

namespace MonDexSharp.Core.Tests;

public static class Factories
{
    public static PokemonSpecies CreatePokemonSpecies(int id, string name = "FooBar", PokemonStats stats = new())
    {
        return PokemonSpecies.Create(id, name, stats);
    }
}
