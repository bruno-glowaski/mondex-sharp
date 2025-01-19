using System.ComponentModel.DataAnnotations;

namespace MonDexSharp.Backend.Dtos;

public record struct PokemonStatsDto(
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
);
