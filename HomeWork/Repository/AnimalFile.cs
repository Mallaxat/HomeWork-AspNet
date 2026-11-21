using System.Text.Json;

namespace HomeWork;

internal interface IAnimalFile
{
    void Save(string path, Animal[] animals);
    Animal[] Load(string path);
}

internal sealed class JsonAnimalFile : IAnimalFile
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true,
        AllowOutOfOrderMetadataProperties = true
    };

    public void Save(string path, Animal[] animals)
    {
        File.WriteAllText(path, JsonSerializer.Serialize(animals, Options));
    }

    public Animal[] Load(string path)
    {
        using var stream = File.OpenRead(path);
        var animals = JsonSerializer.Deserialize<Animal[]>(stream, Options);
        if (animals is null || animals.Any(animal => animal is null || string.IsNullOrWhiteSpace(animal.Name)))
        {
            throw new JsonException("Expected an array of animals with names.");
        }

        return animals;
    }
}
