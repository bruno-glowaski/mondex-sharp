using System.ComponentModel.DataAnnotations;
using MonDexSharp.Core.ValueObjects;

namespace MonDexSharp.Backend.Models;

public readonly record struct PokemonStatsModel(
    [Range(0, int.MaxValue)]
    int HP,
    [Range(0, int.MaxValue)]
    int Attack,
    [Range(0, int.MaxValue)]
    int Defense,
    [Range(0, int.MaxValue)]
    int SpecialAttack,
    [Range(0, int.MaxValue)]
    int SpecialDefense,
    [Range(0, int.MaxValue)]
    int Speed
)
{
    public PokemonStatsModel(PokemonStats src) : this(src.HP, src.Attack, src.Defense, src.SpecialAttack, src.SpecialDefense, src.Speed) { }

    public readonly PokemonStats ToDomain()
    {
        return new(HP, Attack, Defense, SpecialAttack, SpecialDefense, Speed);
    }
}
