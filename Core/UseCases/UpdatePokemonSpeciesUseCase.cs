using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Core.UseCases;

public class UpdatePokemonSpeciesUseCase(IPokemonSpeciesRepository speciesRepository)
{
    public abstract record Result
    {
        private Result() { }

        public sealed record Success() : Result;
        public sealed record NotFound() : Result;
    }

    public async Task<Result> Execute(PokemonSpecies species)
    {
        PokemonSpecies? target = await speciesRepository.GetById(species.Id!.Value);
        if (target == null)
        {
            return new Result.NotFound();
        }
        await speciesRepository.Update(species);
        return new Result.Success();
    }
}
