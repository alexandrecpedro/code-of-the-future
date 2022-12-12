namespace Programa.Models;

public record Produto 
{
    // ATTRIBUTES
    public required string ID { get;set; }
    public string NomeProduto { get; set; } = default!;
    public int QuantidadeEmEstoque { get;set; } = default!;

    // CONSTRUCTOR

    // METHODS
}