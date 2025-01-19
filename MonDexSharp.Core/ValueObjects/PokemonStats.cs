using PokemonStat = int;

namespace MonDexSharp.Core.ValueObjects;

public record struct PokemonStats(
    PokemonStat HP,
    PokemonStat Attack,
    PokemonStat Defense,
    PokemonStat SpecialAttack,
    PokemonStat SpecialDefense,
    PokemonStat Speed
);
