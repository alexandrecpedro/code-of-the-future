using Database.Atributos;

namespace Business.Entidades;

[Tabela(Nome = "fornecedores")]
public class Fornecedor
{
    public int Id { get; set; }
    
    [Coluna(Nome = "razao_social")]
    public string? Nome { get; set; }
    public string? Email { get; set; }
}