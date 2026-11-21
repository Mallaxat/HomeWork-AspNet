namespace HomeWork;

internal sealed class AnimalDisplay(TextWriter output)
{
    public void ShowName(Animal animal) => output.WriteLine(animal.Name);

    public void ShowSound(Animal animal) => output.WriteLine(animal.MakeSound());
}
