using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.Tests.Factories;
using MonDexSharp.Core.UseCases;

namespace MonDexSharp.Core.Tests.Units.UseCases;

public class DeletePokemonSpeciesUseCaseTests
{
    [Fact]
    public async Task DeletingExistingEntries()
    {
        SpeciesFactory speciesFactory = new();
        PokemonSpecies species = speciesFactory.CreatePikachu();
        Mock<IPokemonSpeciesRepository> speciesRepository = new(MockBehavior.Strict);
        _ = speciesRepository.Setup(r => r.GetById(species.Id).Result).Returns(species);
        _ = speciesRepository.Setup(r => r.DeleteById(species.Id)).Returns(Task.CompletedTask);
        DeletePokemonSpeciesUseCase useCase = new(speciesRepository.Object);

        Assert.Equal(await useCase.Execute(species.Id), new DeletePokemonSpeciesUseCase.Result.Success());

        speciesRepository.Verify(r => r.DeleteById(species.Id), Times.Once());
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
