using System.ComponentModel.DataAnnotations;

namespace MonDexSharp.Backend.Dtos;

public record PokemonSpeciesDto(int? Id, [Required][MinLength(1)] string Name, PokemonStatsDto Stats);
