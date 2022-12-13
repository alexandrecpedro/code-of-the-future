namespace School.Models.Abstracts;

public abstract class APerson
{
    // ATTRIBUTES
    public string? Name { get; set; }
    public string? Phone { get; set; }

    // CONSTRUCTOR
    // public APerson() { }

    // METHODS
    public abstract string Serialize();
}