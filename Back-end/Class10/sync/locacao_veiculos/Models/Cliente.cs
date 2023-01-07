namespace locacao_veiculos.Models;

public class Cliente
{
    public int Id { get;set; } = default!;
    public string Nome { get;set; } = default!;
    public string Email { get;set; } = default!;
    public string? Telefone { get;set; }
    public string? Endereco { get;set; }
}
