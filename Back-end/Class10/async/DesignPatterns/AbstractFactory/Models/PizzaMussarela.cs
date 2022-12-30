using AbstractFactory.Models.Abstract;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models;

/// <summary>
/// ProductB2
/// </summary>
public sealed class PizzaMussarela : Pizza
{
    public PizzaMussarela() : base("Pizza Mussarela", TipoMassa.Pizza)
    {
        Ingredientes.Add("Queijo mussarela gratinado e molho de tomate");
    }
}