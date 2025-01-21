using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Tests.Units.Entities;

public class PokemonTypeTests
{
    [Fact]
    public void CanCreateNormalType()
    {
        Assert.NotNull(PokemonType.Create(0, "Normal"));
    }

    [Fact]
    public void CannotCreateNameless()
    {
        _ = Assert.Throws<ValidationException>(static () => PokemonType.Create(0, null!));
    }
}
