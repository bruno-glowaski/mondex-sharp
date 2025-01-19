using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.ValueObjects;

using PokemonId = int;

namespace MonDexSharp.Core.Entities;

public class PokemonSpecies
{
    private PokemonSpecies(int id, string name, PokemonStats baseStats)
    {
        Id = id;
        Name = name;
        BaseStats = baseStats;
    }

    [Range(0, PokemonId.MaxValue)]
    public PokemonId Id { get; }
    [Required]
    public string Name { get; set; }
    public PokemonStats BaseStats { get; set; }

    public static PokemonSpecies Create(PokemonId id, string name, PokemonStats baseStats)
    {
        PokemonSpecies result = new(id, name, baseStats);
        Validator.ValidateObject(result, new(result));
        return result;
    }
}
