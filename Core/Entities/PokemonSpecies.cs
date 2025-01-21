using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.ValueObjects;

namespace MonDexSharp.Core.Entities;

public class PokemonSpecies
{
    private PokemonSpecies(int? id, int number, string name, string genera, string description, IEnumerable<PokemonType> types, PokemonStats baseStats)
    {
        Id = id;
        Number = number;
        Name = name;
        Genera = genera;
        Description = description;
        Types = types.ToList().AsReadOnly();
        BaseStats = baseStats;
    }

    public int? Id { get; }
    [Range(0, int.MaxValue)]
    public int Number { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Genera { get; set; }
    public string Description { get; set; }
    [MinLength(1)]
    [MaxLength(2)]
    public IReadOnlyList<PokemonType> Types { get; }
    public PokemonStats BaseStats { get; set; }

    public static PokemonSpecies Create(int? id, int number, string name, string genera, string description, IEnumerable<PokemonType> types, PokemonStats baseStats)
    {
        PokemonSpecies result = new(id, number, name, genera, description, types, baseStats);
        Validator.ValidateObject(result, new(result));
        return result;
    }
}
