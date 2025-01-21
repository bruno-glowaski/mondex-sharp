using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Data.Models;

public class PokemonSpeciesModel
{
    [Key]
    public required int Id { get; init; }
    public int Number { get; set; }
    public string Name { get; set; } = "";
    public string Genera { get; set; } = "";
    public string Description { get; set; } = "";
    public ICollection<PokemonTypeModel> Types { get; set; } = [];
    public PokemonStatsModel BaseStats { get; set; }

    public PokemonSpecies ToDomain()
    {
        return PokemonSpecies.Create(Id, Number, Name, Genera, Description, Types.Select(static t => t.ToDomain()), BaseStats.ToDomain());
    }
}
