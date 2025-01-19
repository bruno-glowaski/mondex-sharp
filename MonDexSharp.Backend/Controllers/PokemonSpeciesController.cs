using Microsoft.AspNetCore.Mvc;

using MonDexSharp.Backend.Dtos;

namespace MonDexSharp.Backend.Controllers;

[ApiController]
[Route("api/species")]
public class PokemonSpeciesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<PokemonSpecieDto>> Index()
    {
        return Ok(new List<PokemonSpecieDto>() { new(0, "ab", new()) });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PokemonSpecieDto> GetById(int id)
    {
        return id == 0 ? Ok(new PokemonSpecieDto(0, "ab", new())) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PokemonSpecieDto> Create([Bind(["Name", "Stats"])] PokemonSpecieDto specie)
    {
        return CreatedAtAction(nameof(GetById), new { id = 0 }, specie with { Id = 0 });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PokemonSpecieDto> Update(int id, [Bind(["Name", "Stats"])] PokemonSpecieDto specie)
    {
        return id == 0 ?
          CreatedAtAction(nameof(GetById), new { id = 0 }, specie with { Id = 0 }) :
          NotFound();
    }
}
