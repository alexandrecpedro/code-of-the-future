namespace Programa.Models;

public record Produto 
{
    public required string ID { get;set; }
    public string NomeProduto { get; set; } = default!;
    public int QuantidadeEmEstoque { get;set; } = default!;
}