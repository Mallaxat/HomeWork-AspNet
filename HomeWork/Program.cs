using HomeWork;
using Microsoft.Extensions.DependencyInjection;

string command = args.Length == 0 ? "show" : args[0];
string? path = args.Length == 2 ? args[1] : null;

if (args.Length > 2 || command is not ("show" or "save" or "load") ||
    (command is "save" or "load" && string.IsNullOrWhiteSpace(path)) ||
    (path is not null && string.IsNullOrWhiteSpace(path)))
{
    Console.Error.WriteLine("Usage: show [output.txt] | save animals.json | load animals.json");
    return 1;
}

var services = new ServiceCollection();
services.AddSingleton<AnimalDisplay>();
services.AddSingleton<TextWriter>(_ => command == "show" && path is not null
    ? File.CreateText(path)
    : Console.Out);
services.AddKeyedSingleton<IAnimalFile, JsonAnimalFile>(".json");

Animal[] animals = [new Dog("Dog"), new Cat("Cat"), new Cow("Cow")];

try
{
    using var provider = services.BuildServiceProvider();
    if (command is "save" or "load")
    {
        string extension = Path.GetExtension(path!).ToLowerInvariant();
        var file = provider.GetKeyedService<IAnimalFile>(extension);
        if (file is null)
        {
            Console.Error.WriteLine($"Unsupported file format: {extension}");
            return 1;
        }

        if (command == "save")
        {
            file.Save(path!, animals);
            return 0;
        }

        animals = file.Load(path!);
    }

    var display = provider.GetRequiredService<AnimalDisplay>();
    foreach (var animal in animals)
    {
        display.ShowName(animal);
        display.ShowSound(animal);
    }

    return 0;
}
catch (Exception exception) when (exception is IOException or UnauthorizedAccessException
                                  or System.Text.Json.JsonException or ArgumentException)
{
    Console.Error.WriteLine(exception.Message);
    return 1;
}
