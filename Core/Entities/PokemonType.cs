using System.ComponentModel.DataAnnotations;
namespace MonDexSharp.Core.Entities;

public class PokemonType
{
    private PokemonType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Range(0, int.MaxValue)]
    public int Id { get; }
    [Required]
    public string Name { get; set; }

    public static PokemonType Create(int id, string name)
    {
        PokemonType result = new(id, name);
        Validator.ValidateObject(result, new(result));
        return result;
    }
}
