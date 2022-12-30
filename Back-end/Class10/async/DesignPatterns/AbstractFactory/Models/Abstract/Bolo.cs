using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models.Abstract;

/// <summary>
/// AbstractProductA
/// </summary>
public abstract class Bolo : MassaBase
{
    public Bolo(string nome, TipoMassa tipo) : base(nome, tipo) { }
}