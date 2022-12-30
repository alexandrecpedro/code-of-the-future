using AbstractFactory.Models.Abstract;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models;

/// <summary>
/// ProductA2
/// </summary>
public sealed class BoloLaranja : Bolo
{
    public BoloLaranja() : base("Bolo de Laranja", TipoMassa.Bolo)
    {
        Ingredientes.Add("Com cobertura de calda de laranja");
    }
}