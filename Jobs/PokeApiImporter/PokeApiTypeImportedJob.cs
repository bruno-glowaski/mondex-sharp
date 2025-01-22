using Microsoft.Extensions.Logging;
using MonDexSharp.Core.Entities;
using MonDexSharp.Core.Interfaces.Repositories;

namespace MonDexSharp.Jobs.PokeApiImporter;

public class PokeApiTypeImporterJob(ILogger<PokeApiTypeImporterJob> logger, PokeApiNet.PokeApiClient client, IPokemonTypeRepository typeRepository)
{
    public async Task Run(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting Pokémon Type import job.");
        List<PokeApiNet.NamedApiResource<PokeApiNet.Type>> rawTypes =
          await client
            .GetAllNamedResourcesAsync<PokeApiNet.Type>(cancellationToken).ToListAsync(cancellationToken);
        foreach (PokeApiNet.Type item in await client.GetResourceAsync(rawTypes, cancellationToken))
        {
            string? name = item.Names.FirstOrDefault(i => i.Language.Name == "en").Name;
            if (name == null)
            {
                logger.LogInformation("Type with resource name {ResourceName} has no English name. Skipping.");
                continue;
            }
            logger.LogInformation("Imported type '{Name}'", name);
            // Should probably add a cancellationToken param to these. Oh well.
            if (await typeRepository.GetByName(name) != null)
            {
                // Update type here. Since it just holds name, we do nothing.
                continue;
            }
            _ = await typeRepository.Create(PokemonType.Create(null, name));
        }
        logger.LogInformation("Terminating Pokémon Type import job.");
    }
}
