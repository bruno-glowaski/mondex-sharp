using Microsoft.AspNetCore.Mvc;
using MonDexSharp.Backend.Dtos;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;

namespace MonDexSharp.Backend.Controllers;

[ApiController]
[Route("api/species")]
public class PokemonSpeciesController(IPokemonSpeciesRepository speciesRepository) : ControllerBase
{
    private readonly IPokemonSpeciesRepository speciesRepository = speciesRepository;
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PokemonSpeciesDto>>> Index()
    {
        return Ok(await speciesRepository.All());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonSpeciesDto>> GetById(int id)
    {
        PokemonSpecies? species = await speciesRepository.GetById(id);
        return species != null ? Ok(new PokemonSpeciesDto(species!)) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PokemonSpeciesDto>> Create(PokemonSpeciesDto data, [FromServices] CreatePokemonSpeciesUseCase useCase)
    {
        PokemonSpecies entity = data.ToEntity();
        await useCase.Execute(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new PokemonSpeciesDto(entity));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonSpeciesDto>> Update(int id, PokemonSpeciesDto data, [FromServices] UpdatePokemonSpeciesUseCase useCase)
    {
        return await useCase.Execute(id, data.ToEntity()) switch
        {
            UpdatePokemonSpeciesUseCase.Result.Success => Ok(),
            UpdatePokemonSpeciesUseCase.Result.NotFound => NotFound(),
            UpdatePokemonSpeciesUseCase.Result.ChangedId => BadRequest(),
            _ => throw new InvalidOperationException()
        };
    }
}
