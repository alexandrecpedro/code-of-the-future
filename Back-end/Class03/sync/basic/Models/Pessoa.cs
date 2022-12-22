using Basico.Abstracao;

namespace Basico.Models;

public class Pessoa : AObjeto
{
    // ATTRIBUTES

    // CONSTRUCTOR
    public Pessoa() { }

    public Pessoa(string _nome)
    {
        this.Nome = _nome;
    }

    // METHODS
    public override string NomeMaiusculo()
    {
        return this.Nome.ToUpper() + " - Classe Pessoa";
    }

    public override string NomeMinusculo()
    {
        return this.Nome.ToLower();
    }
}