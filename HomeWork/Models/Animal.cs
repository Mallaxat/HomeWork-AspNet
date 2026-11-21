using System.Text.Json.Serialization;

namespace HomeWork;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Cat), "cat")]
[JsonDerivedType(typeof(Cow), "cow")]
public abstract record Animal(string Name)
{
    public abstract string MakeSound();
}

public sealed record Dog(string Name) : Animal(Name)
{
    public override string MakeSound() => "Woof!";
}

public sealed record Cat(string Name) : Animal(Name)
{
    public override string MakeSound() => "Meow!";
}

public sealed record Cow(string Name) : Animal(Name)
{
    public override string MakeSound() => "Moo!";
}
