using Basico.Interfaces;

namespace Basico.Abstracao;

public abstract class AObjeto : IObjeto
{
    // ATTRIBUTES
    public string Nome { get; set; } = default!;

    // METHODS
    public abstract string NomeMaiusculo();
    public virtual string NomeMinusculo()
    {
        return this.Nome.ToLower();
    }
}