using Microsoft.AspNetCore.Mvc;
using MonDexSharp.Backend.Dtos;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Backend.Controllers;

[ApiController]
[Route("api/types")]
public class PokemonTypesController(IPokemonTypeRepository typeRepository) : ControllerBase
{
    private readonly IPokemonTypeRepository typeRepository = typeRepository;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PokemonTypeDto>> Create(UpsertPokemonTypeDto data)
    {
        PokemonType entity = await typeRepository.Create(data.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new PokemonTypeDto(entity));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PokemonTypeDto>>> Index([FromQuery] string? q = null)
    {
        return Ok(await typeRepository.All(name: q));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonTypeDto>> GetById(int id)
    {
        PokemonType? entity = await typeRepository.GetById(id);
        return entity != null ? Ok(new PokemonTypeDto(entity!)) : NotFound();
    }
}
