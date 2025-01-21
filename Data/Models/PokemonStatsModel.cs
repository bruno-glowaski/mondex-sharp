using MonDexSharp.Core.ValueObjects;

namespace MonDexSharp.Data.Models;

public readonly record struct PokemonStatsModel(
    int HP,
    int Attack,
    int Defense,
    int SpecialAttack,
    int SpecialDefense,
    int Speed
)
{
    public PokemonStatsModel(PokemonStats src) : this(src.HP, src.Attack, src.Defense, src.SpecialAttack, src.SpecialDefense, src.Speed) { }

    public readonly PokemonStats ToDomain()
    {
        return new(HP, Attack, Defense, SpecialAttack, SpecialDefense, Speed);
    }
}
