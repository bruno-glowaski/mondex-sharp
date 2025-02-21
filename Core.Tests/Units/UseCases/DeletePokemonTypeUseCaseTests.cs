using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;
using MonDexSharp.Core.Tests.Factories;
using MonDexSharp.Core.UseCases;

namespace MonDexSharp.Core.Tests.Units.UseCases;

public class DeletePokemonTypeUseCaseTests
{
    [Fact]
    public async Task DeletingExistingEntries()
    {
        TypeFactory typeFactory = new();
        PokemonType type = typeFactory.Electric;
        int typeId = type.Id!.Value;
        Mock<IPokemonTypeRepository> typeRepository = new(MockBehavior.Strict);
        _ = typeRepository.Setup(r => r.GetById(typeId).Result).Returns(type);
        _ = typeRepository.Setup(r => r.DeleteById(typeId)).Returns(Task.CompletedTask);
        Mock<IPokemonSpeciesRepository> speciesRepository = new(MockBehavior.Strict);
        _ = speciesRepository.Setup(r => r.AnyWithTypeId(typeId)).ReturnsAsync(false);
        DeletePokemonTypeUseCase useCase = new(typeRepository.Object, speciesRepository.Object);

        Assert.Equal(await useCase.Execute(typeId), new DeletePokemonTypeUseCase.Result.Success());

        typeRepository.Verify(r => r.DeleteById(typeId), Times.Once());
    }

    [Fact]
    public async Task DeletingNonexistingEntries()
    {
        Mock<IPokemonTypeRepository> typeRepository = new(MockBehavior.Strict);
        _ = typeRepository.Setup(static r => r.GetById(It.IsAny<int>()).Result).Returns(static () => null);
        Mock<IPokemonSpeciesRepository> speciesRepository = new(MockBehavior.Strict);
        DeletePokemonTypeUseCase useCase = new(typeRepository.Object, speciesRepository.Object);

        Assert.Equal(await useCase.Execute(0), new DeletePokemonTypeUseCase.Result.NotFound());

        typeRepository.Verify(static r => r.DeleteById(It.IsAny<int>()), Times.Never());
    }

    [Fact]
    public async Task DeletingWithDependingSpecies()
    {
        TypeFactory typeFactory = new();
        PokemonType type = typeFactory.Electric;
        int typeId = type.Id!.Value;
        Mock<IPokemonTypeRepository> typeRepository = new(MockBehavior.Strict);
        _ = typeRepository.Setup(r => r.GetById(typeId).Result).Returns(type);
        Mock<IPokemonSpeciesRepository> speciesRepository = new(MockBehavior.Strict);
        _ = speciesRepository.Setup(r => r.AnyWithTypeId(typeId)).ReturnsAsync(true);
        DeletePokemonTypeUseCase useCase = new(typeRepository.Object, speciesRepository.Object);

        Assert.Equal(await useCase.Execute(typeId), new DeletePokemonTypeUseCase.Result.WillOrphanPokemons());

        typeRepository.Verify(static r => r.DeleteById(It.IsAny<int>()), Times.Never());
    }
}
