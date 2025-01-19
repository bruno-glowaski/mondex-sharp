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
        public sealed record ChangedId() : Result;
    }

    public async Task<Result> Execute(int id, PokemonSpecies species)
    {
        if (species.Id != id)
        {
            return new Result.ChangedId();
        }

        PokemonSpecies? target = await speciesRepository.GetById(id);
        if (target == null)
        {
            return new Result.NotFound();
        }

        // target.Name = species.Name;
        // target.BaseStats = species.BaseStats;
        await speciesRepository.Update(species);
        return new Result.Success();
    }
}
