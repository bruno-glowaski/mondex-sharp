using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Core.UseCases;

public class DeletePokemonSpeciesUseCase(IPokemonSpeciesRepository speciesRepository)
{
    public abstract record Result
    {
        private Result() { }

        public sealed record Success() : Result;
        public sealed record NotFound() : Result;
    }

    public async Task<Result> Execute(int speciesId)
    {
        if (await speciesRepository.GetById(speciesId) == null)
        {
            return new Result.NotFound();
        }
        await speciesRepository.DeleteById(speciesId);
        return new Result.Success();
    }
}
