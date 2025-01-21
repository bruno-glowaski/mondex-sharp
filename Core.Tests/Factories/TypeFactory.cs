using MonDexSharp.Core.Entities;

namespace MonDexSharp.Core.Tests.Factories;

public class TypeFactory
{
    private readonly PokemonType[] types = [
        PokemonType.Create(0, "Normal"),
        PokemonType.Create(1, "Fire"),
        PokemonType.Create(2, "Fighting"),
        PokemonType.Create(3, "Water"),
        PokemonType.Create(4, "Flying"),
        PokemonType.Create(5, "Grass"),
        PokemonType.Create(6, "Poison"),
        PokemonType.Create(7, "Electric"),
        PokemonType.Create(8, "Ground"),
        PokemonType.Create(9, "Psychic"),
        PokemonType.Create(10, "Rock"),
        PokemonType.Create(11, "Ice"),
        PokemonType.Create(12, "Bug"),
        PokemonType.Create(13, "Dragon"),
        PokemonType.Create(14, "Ghost"),
        PokemonType.Create(15, "Dark"),
        PokemonType.Create(16, "Steel"),
        PokemonType.Create(17, "Fairy"),
    ];

    public PokemonType Normal => types[0];
    public PokemonType Fire => types[1];
    public PokemonType Fighting => types[2];
    public PokemonType Water => types[3];
    public PokemonType Flying => types[4];
    public PokemonType Grass => types[5];
    public PokemonType Poison => types[6];
    public PokemonType Electric => types[7];
    public PokemonType Ground => types[8];
    public PokemonType Psychic => types[9];
    public PokemonType Rock => types[10];
    public PokemonType Ice => types[11];
    public PokemonType Bug => types[12];
    public PokemonType Dragon => types[13];
    public PokemonType Ghost => types[14];
    public PokemonType Dark => types[15];
    public PokemonType Steel => types[16];
    public PokemonType Fairy => types[17];
}
