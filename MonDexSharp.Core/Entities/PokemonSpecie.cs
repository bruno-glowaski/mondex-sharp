using MonDexSharp.Core.ValueObjects;

using PokemonId = int;

namespace MonDexSharp.Core.Entities;

public class PokemonSpecie
{
    public PokemonSpecie(int id, string name, PokemonStats baseStats)
    {
        Id = id;
        this.name = name;
        Name = name;
        BaseStats = baseStats;
    }

    public PokemonId Id { get; }
    public string Name
    {
        get => name; set
        {
            name = value.Trim();
            if (name.Length == 0)
            {
                throw new ArgumentException("Tried to create a nameless Pokémon");
            }
        }
    }
    public PokemonStats BaseStats { get; set; }

    private string name;
}
