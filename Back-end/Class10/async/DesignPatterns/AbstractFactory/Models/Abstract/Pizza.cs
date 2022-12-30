using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models.Abstract;

/// <summary>
/// AbstractProductB
/// </summary>
public abstract class Pizza : MassaBase
{
    public Pizza(string nome, TipoMassa tipo) : base(nome, tipo) { }
}