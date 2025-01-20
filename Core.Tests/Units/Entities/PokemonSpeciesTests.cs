using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Tests.Units.Entities;

public class PokemonSpeciesTests
{
    [Fact]
    public void CannotCreateNameless()
    {
        Assert.NotNull(static () => PokemonSpecies.Create(0, "abs", new()));
        _ = Assert.Throws<ValidationException>(static () => PokemonSpecies.Create(0, null!, new()));
        _ = Assert.Throws<ValidationException>(static () => PokemonSpecies.Create(0, "", new()));
        _ = Assert.Throws<ValidationException>(static () => PokemonSpecies.Create(0, " ", new()));
    }
}
