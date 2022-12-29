namespace Database.Atributos;

public class ColunaAttribute : Attribute
{
    public ColunaAttribute()
    {
    }

    public string Nome { get; set; } = default!;
}