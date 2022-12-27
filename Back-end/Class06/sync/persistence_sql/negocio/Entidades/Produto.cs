namespace Negocio.Entidades;

public record Produto
{
    public string Id { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataValidade { get; set; }
    public int QuantidadeEstoque { get; set; }
}