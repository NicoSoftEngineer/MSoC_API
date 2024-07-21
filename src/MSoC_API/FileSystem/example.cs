namespace MSoC_API.FileSystem;

// Class declaration
public class Animal : IAnimal
{
    // Fields
    private string name;
    private int age;

    // Property
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Property
    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    // Property using auto-implemented property
    public AnimalType Type { get; set; }

    // Constructor
    public Animal(string name, int age, AnimalType type)
    {
        this.name = name;
        this.age = age;
        this.Type = type;
    }

    // Method
    public void MakeSound()
    {
        switch (Type)
        {
            case AnimalType.Dog:
                Console.WriteLine($"{Name} says: Woof!");
                break;
            case AnimalType.Cat:
                Console.WriteLine($"{Name} says: Meow!");
                break;
            case AnimalType.Bird:
                Console.WriteLine($"{Name} says: Tweet!");
                break;
            case AnimalType.Fish:
                Console.WriteLine($"{Name} makes no sound.");
                break;
        }
    }

    // Method
    public void Move()
    {
        switch (Type)
        {
            case AnimalType.Dog:
            case AnimalType.Cat:
            case AnimalType.Bird:
                Console.WriteLine($"{Name} is moving.");
                break;
            case AnimalType.Fish:
                Console.WriteLine($"{Name} is swimming.");
                break;
        }
    }
}
public enum AnimalType
{
    Dog,
    Cat,
    Bird,
    Fish
}

// Interface declaration
public interface IAnimal
{
    void MakeSound();
    void Move();
}

// Main program to demonstrate usage
public class Program
{
    public void Main(string[] args)
    {
        // Creating objects of Animal class
        Animal dog = new Animal("Buddy", 3, AnimalType.Dog);
        Animal cat = new Animal("Whiskers", 2, AnimalType.Cat);
        Animal bird = new Animal("Tweety", 1, AnimalType.Bird);
        Animal fish = new Animal("Goldie", 1, AnimalType.Fish);

        // Using properties and methods
        dog.MakeSound();
        dog.Move();

        cat.MakeSound();
        cat.Move();

        bird.MakeSound();
        bird.Move();

        fish.MakeSound();
        fish.Move();
    }
}