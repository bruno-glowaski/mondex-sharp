using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Tests.Factories;

namespace MonDexSharp.Core.Tests.Units.Entities;

public class PokemonSpeciesTests
{
    private readonly TypeFactory types = new();

    [Fact]
    public void Pikachu()
    {
        Assert.NotNull(PokemonSpecies.Create(
          0,
          25,
          "Pikachu",
          "Mouse Pokémon",
          "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
          [types.Electric],
          new(35, 55, 40, 50, 50, 90)
        ));
    }
    [Fact]
    public void NeedsName()
    {
        _ = Assert.Throws<ValidationException>(() => PokemonSpecies.Create(
          0,
          27,
          null!,
          "Mouse Pokémon",
          "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
          [types.Electric],
          new()
        ));
        _ = Assert.Throws<ValidationException>(() => PokemonSpecies.Create(
          0,
          27,
          "",
          "Mouse Pokémon",
          "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
          [types.Electric],
          new()
        ));
        _ = Assert.Throws<ValidationException>(() => PokemonSpecies.Create(
          0,
          27,
          " ",
          "Mouse Pokémon",
          "When it is angered, it immediately discharges the energy stored in the pouches in its cheeks.",
          [types.Electric],
          new()
        ));
    }
}
