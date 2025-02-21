using Microsoft.AspNetCore.Mvc;
using MonDexSharp.Backend.Dtos;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;

namespace MonDexSharp.Backend.Controllers;

[ApiController]
[Route("api/types")]
public class PokemonTypesController(IPokemonTypeRepository typeRepository) : ControllerBase
{
    private readonly IPokemonTypeRepository typeRepository = typeRepository;

    [HttpPost(Name = "CreatePokemonType")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PokemonTypeDto>> Create(UpsertPokemonTypeDto data)
    {
        PokemonType entity = await typeRepository.Create(data.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new PokemonTypeDto(entity));
    }

    [HttpGet(Name = "GetPokemonTypes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PokemonTypeDto>>> Index([FromQuery] string? q = null)
    {
        return Ok(await typeRepository.All(name: q));
    }

    [HttpGet("{id}", Name = "GetPokemonType")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PokemonTypeDto>> GetById(int id)
    {
        PokemonType? entity = await typeRepository.GetById(id);
        return entity != null ? Ok(new PokemonTypeDto(entity!)) : NotFound();
    }

    [HttpPut("{id}", Name = "UpdatePokemonType")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, UpsertPokemonTypeDto data)
    {
        PokemonType? entity = await typeRepository.GetById(id);
        if (entity == null)
        {
            return NotFound();
        }
        entity.Name = data.Name;
        await typeRepository.Update(entity);
        return entity != null ? Ok(new PokemonTypeDto(entity!)) : NotFound();
    }

    [HttpDelete("{id}", Name = "DeletePokemonType")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id, [FromServices] DeletePokemonTypeUseCase useCase)
    {
        switch (await useCase.Execute(id))
        {
            case DeletePokemonTypeUseCase.Result.Success:
                return Ok();
            case DeletePokemonTypeUseCase.Result.NotFound:
                return NotFound();
            case DeletePokemonTypeUseCase.Result.WillOrphanPokemons:
                ModelState.AddModelError("", "There are species with this type. Delete them first");
                return UnprocessableEntity(ModelState);
            default:
                throw new InvalidOperationException();
        }
    }
}
