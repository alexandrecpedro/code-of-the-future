namespace School.Models.Interfaces;

interface IPerson
{
    // ATTRIBUTES
    string? Name { get; set; }
    string? Phone { get; set; }

    // METHODS
    string Serialize();
}