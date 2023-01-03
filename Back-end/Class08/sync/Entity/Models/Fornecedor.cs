namespace Entity.Models;

public record Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public string? Email { get; set; }
}