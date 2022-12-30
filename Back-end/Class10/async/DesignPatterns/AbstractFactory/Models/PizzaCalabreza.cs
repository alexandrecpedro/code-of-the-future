using AbstractFactory.Models.Abstract;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models;

/// <summary>
/// ProductB1
/// </summary>
public sealed class PizzaCalabreza : Pizza
{
    public PizzaCalabreza() : base("Pizza Calabreza", TipoMassa.Pizza)
    {
        Ingredientes.Add("Calabreza em cubos e tomates em cubos");
    }
}