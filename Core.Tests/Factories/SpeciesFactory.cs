using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Tests.Factories;

public class SpeciesFactory
{
    private readonly TypeFactory types = new();
    private int nextId = 1;

    public PokemonSpecies CreatePikachu()
    {
        return PokemonSpecies.Create(
          GetNextId(),
          25,
          "Pikachu",
          "Mouse Pokémon",
          "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
          [types.Electric],
          new(35, 55, 40, 50, 50, 90)
        );
    }

    private int GetNextId()
    {
        return nextId++;
    }
}
