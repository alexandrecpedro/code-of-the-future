using AbstractFactory.Models.Abstract;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models;

/// <summary>
/// ProductA1
/// </summary>
public sealed class BoloChocolate : Bolo
{
    public BoloChocolate() : base("Bolo de Chocolate", TipoMassa.Bolo)
    {
        Ingredientes.Add("Com cobertura de chocolate Nestlé");
    }
}