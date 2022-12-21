namespace Business.Models;

public class Fornecedor : Pessoa
{
    public string CNPJ { get; set; } = default!;

    public Fornecedor(string Nome, string CNPJ) {
        this.Id = new Guid().ToString();
        this.Nome = Nome;
        this.Tipo = "Pessoa Jurídica";
        this.CNPJ = CNPJ;
    }

    public void CadastrarFornecedor()
    {
        var fornecedor = new Fornecedor(Nome, CNPJ);
        base.Cadastrar(fornecedor);
    }
}