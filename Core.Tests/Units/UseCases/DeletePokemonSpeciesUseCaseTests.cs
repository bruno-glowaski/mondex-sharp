using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.UseCases;

namespace MonDexSharp.Core.Tests.Units.UseCases;

public class DeletePokemonSpeciesUseCaseTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async Task DeletingExistingEntries(int speciesId)
    {
        Mock<IPokemonSpeciesRepository> speciesRepository = new(MockBehavior.Strict);
        _ = speciesRepository.Setup(r => r.GetById(speciesId).Result).Returns(PokemonSpecies.Create(speciesId, "FooBar", new()));
        _ = speciesRepository.Setup(r => r.DeleteById(speciesId)).Returns(Task.CompletedTask);
        DeletePokemonSpeciesUseCase useCase = new(speciesRepository.Object);

        Assert.Equal(await useCase.Execute(speciesId), new DeletePokemonSpeciesUseCase.Result.Success());

        speciesRepository.Verify(r => r.DeleteById(speciesId), Times.Once());
    }

    [Fact]
    public async Task DeletingNonexistingEntries()
    {
        Mock<IPokemonSpeciesRepository> speciesRepository = new(MockBehavior.Strict);
        _ = speciesRepository.Setup(static r => r.GetById(It.IsAny<int>()).Result).Returns(static () => null!);
        _ = speciesRepository.Setup(static r => r.DeleteById(It.IsAny<int>())).Returns(Task.CompletedTask);
        DeletePokemonSpeciesUseCase useCase = new(speciesRepository.Object);

        Assert.Equal(await useCase.Execute(0), new DeletePokemonSpeciesUseCase.Result.NotFound());

        speciesRepository.Verify(static r => r.DeleteById(It.IsAny<int>()), Times.Never());
    }
}
