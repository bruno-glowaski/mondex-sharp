using System.ComponentModel.DataAnnotations;

namespace MonDexSharp.Backend.Dtos;

public record PokemonSpecieDto(int? Id, [Required][MinLength(1)] string Name, PokemonStatsDto Stats);
