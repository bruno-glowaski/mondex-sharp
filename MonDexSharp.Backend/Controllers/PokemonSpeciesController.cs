using Microsoft.AspNetCore.Mvc;
using MonDexSharp.Backend.Models;
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
    public async Task<ActionResult<List<PokemonSpeciesModel>>> Index()
    {
        return Ok(await speciesRepository.All());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonSpeciesModel>> GetById(int id)
    {
        PokemonSpecies? species = await speciesRepository.GetById(id);
        return species != null ? Ok(new PokemonSpeciesModel(species!)) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PokemonSpeciesModel>> Create(PokemonSpeciesModel data, [FromServices] CreatePokemonSpeciesUseCase useCase)
    {
        PokemonSpecies entity = data.ToDomain();
        await useCase.Execute(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new PokemonSpeciesModel(entity));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonSpeciesModel>> Update(int id, PokemonSpeciesModel data, [FromServices] UpdatePokemonSpeciesUseCase useCase)
    {
        return await useCase.Execute(id, data.ToDomain()) switch
        {
            UpdatePokemonSpeciesUseCase.Result.Success => Ok(),
            UpdatePokemonSpeciesUseCase.Result.NotFound => NotFound(),
            UpdatePokemonSpeciesUseCase.Result.ChangedId => BadRequest(),
            _ => throw new InvalidOperationException()
        };
    }
}
