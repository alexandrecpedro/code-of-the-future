namespace Basico.Models;

public class Fornecedor : Pessoa
{
    // ATTRIBUTES
    public string Telefone { get;set; } = default!;

    // CONSTRUCTOR
    public Fornecedor() { }

    public Fornecedor(string nome): base(nome) { }

    // METHODS
    public override string? ToString()
    {
        return this.Nome;
    }

    public override string NomeMinusculo()
    {
        return this.Nome.ToLower();
    }

    public override string NomeMaiusculo()
    {
        return this.Nome.ToUpper() + " --- " + base.NomeMaiusculo();
    }
}