using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Core.UseCases;

public class CreatePokemonSpeciesUseCase(IPokemonSpeciesRepository speciesRepository)
{
    public abstract record Result
    {
        private Result() { }

        public sealed record Success() : Result;
        public sealed record NotFound() : Result;
        public sealed record ChangedId() : Result;
    }

    public async Task Execute(PokemonSpecies species)
    {
        await speciesRepository.Create(species);
    }
}
