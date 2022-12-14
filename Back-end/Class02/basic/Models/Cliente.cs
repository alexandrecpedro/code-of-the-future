namespace Basico.Models;

public class Cliente
{
    // ATTRIBUTES
    public string Nome { get;set; } = default!;
    public string Telefone { get;set; } = default!;

    // CONSTRUCTOR
    public Cliente()
    {}

    public Cliente(string _nome, string _telefone)
    {
        this.Nome = _nome;
        this.Telefone = _telefone;
    }

    // METHODS
    public string ClientePorCompleto()
    {
        return $"Nome: {this.Nome} - Telefone: {this.Telefone}";
    }

    public string ClientePorCompleto(string mensagem)
    {
        return $"{mensagem} - Nome: {this.Nome} - Telefone: {this.Telefone}";
    }

    internal string ClientePorCompleto2()
    {
        return $"Nome: {this.Nome} - Telefone: {this.Telefone}";
    }

    protected string ClientePorCompleto3()
    {
        return $"Nome: {this.Nome} - Telefone: {this.Telefone}";
    }

    private string ClientePorCompleto4()
    {
        return $"Nome: {this.Nome} - Telefone: {this.Telefone}";
    }
}