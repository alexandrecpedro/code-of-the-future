namespace Basico.Models;

public class Unica
{
    // ATTRIBUTES
    public string Teste { get; set; } = default!;
    public string Teste2 { get; set; } = default!;

    // CONSTRUCTOR
    private Unica() { }

    // METHODS
    private static Unica unica = new Unica();
    public static Unica Get()
    {
        return unica;
    }
}