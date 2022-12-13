using School.Models.Interfaces;
using School.Models.Abstracts;

namespace School.Models;

public class Person : APerson
{
    // ATTRIBUTES
    public string? Name { get; set; }
    public string? Phone { get; set; }
    internal string privateItem { get; set; } = "Default hello";

    // CONSTRUCTOR
    // public Person() { }

    // METHODS
    public virtual void Save()
    {
        Console.WriteLine($"Base method: {this.privateItem}");
    }

    public override string Serialize()
    {
        return "Serialized";
    }
}