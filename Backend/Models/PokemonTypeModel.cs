using MonDexSharp.Core.Entities;

namespace MonDexSharp.Backend.Models;

public class PokemonTypeModel
{
    public PokemonTypeModel() { Name = ""; } // EF Core
    public PokemonTypeModel(PokemonType entity)
    {
        Id = entity.Id ?? 0;
        Name = entity.Name;
    }

    public int Id { get; }
    public string Name { get; set; }
    public ICollection<PokemonSpeciesModel> Species { get; } = [];

    public PokemonType ToDomain()
    {
        return PokemonType.Create(Id, Name);
    }
}
