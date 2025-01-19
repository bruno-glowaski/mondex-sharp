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
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
    public PokemonStatsModel BaseStats { get; set; }

    public PokemonSpecies ToDomain()
    {
        return PokemonSpecies.Create(Id, Name, BaseStats.ToDomain());
    }
}
