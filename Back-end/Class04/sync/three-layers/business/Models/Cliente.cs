namespace Business.Models;

public class Cliente : Pessoa
{
    public string CPF { get; set; } = default!;

    public Cliente(string Nome, string CPF) {
        this.Id = new Guid().ToString();
        this.Nome = Nome;
        this.Tipo = "Pessoa Física";
        this.CPF = CPF;
    }

    public void CadastrarCliente()
    {
        var cliente = new Cliente(Nome, CPF);
        base.Cadastrar(cliente);
    }
}