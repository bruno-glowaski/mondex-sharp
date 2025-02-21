using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Core.UseCases;

public class DeletePokemonTypeUseCase(IPokemonTypeRepository typeRepository, IPokemonSpeciesRepository speciesRepository)
{
    public abstract record Result
    {
        private Result() { }

        public sealed record Success() : Result;
        public sealed record WillOrphanPokemons() : Result;
        public sealed record NotFound() : Result;
    }

    public async Task<Result> Execute(int typeId)
    {
        if (await typeRepository.GetById(typeId) == null)
        {
            return new Result.NotFound();
        }
        if (await speciesRepository.AnyWithTypeId(typeId))
        {
            return new Result.WillOrphanPokemons();
        }
        await typeRepository.DeleteById(typeId);
        return new Result.Success();
    }
}
