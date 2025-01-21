using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Models;

public class PokemonSpeciesModel
{
    public PokemonSpeciesModel() { }
    public PokemonSpeciesModel(PokemonSpecies entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        BaseStats = new(entity.BaseStats);
    }

    [Key]
    public int Id { get; }
    public required int Number { get; set; }
    public required string Name { get; set; }
    public required string Genera { get; set; }
    public required string Description { get; set; }
    public required ICollection<PokemonTypeModel> Types { get; set; }
    public PokemonStatsModel BaseStats { get; set; }

    public PokemonSpecies ToDomain()
    {
        return PokemonSpecies.Create(Id, Number, Name, Genera, Description, Types.Select(static t => t.ToDomain()), BaseStats.ToDomain());
    }
}
