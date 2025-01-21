using Microsoft.AspNetCore.Mvc;
using MonDexSharp.Backend.Dtos;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;

namespace MonDexSharp.Backend.Controllers;

[ApiController]
[Route("api/species")]
public class PokemonSpeciesController(IPokemonSpeciesRepository speciesRepository, IPokemonTypeRepository typeRepository) : ControllerBase
{
    private readonly IPokemonSpeciesRepository speciesRepository = speciesRepository;
    private readonly IPokemonTypeRepository typeRepository = typeRepository;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PokemonSpeciesDto>> Create(UpsertPokemonSpeciesDto data, [FromServices] CreatePokemonSpeciesUseCase useCase)
    {
        PokemonSpecies entity = await useCase.Execute(await data.ToDomain(typeRepository));
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new PokemonSpeciesDto(entity));
    }

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
        return species != null ? Ok(new PokemonSpeciesDto(species)) : NotFound();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonSpeciesDto>> Update(int id, UpsertPokemonSpeciesDto data, [FromServices] UpdatePokemonSpeciesUseCase useCase)
    {
        PokemonSpecies entity;
        try
        {
            entity = await data.ToDomain(typeRepository, withId: id);
        }
        catch (KeyNotFoundException e)
        {
            ModelState.AddModelError(nameof(data.Types), e.Message);
            return ValidationProblem();
        }
        return await useCase.Execute(entity) switch
        {
            UpdatePokemonSpeciesUseCase.Result.Success => Ok(),
            UpdatePokemonSpeciesUseCase.Result.NotFound => NotFound(),
            _ => throw new InvalidOperationException()
        };
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id, [FromServices] DeletePokemonSpeciesUseCase useCase)
    {
        return await useCase.Execute(id) switch
        {
            DeletePokemonSpeciesUseCase.Result.Success => Ok(),
            DeletePokemonSpeciesUseCase.Result.NotFound => NotFound(),
            _ => throw new InvalidOperationException()
        };
    }
}
