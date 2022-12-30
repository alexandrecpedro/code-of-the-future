using System.Collections;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.Models.Abstract;

public abstract class MassaBase
{
    public TipoMassa TipoMassa { get; set; }
    public string Nome { get; set; } = default!;

    public ArrayList Ingredientes = new ArrayList();

    public MassaBase(string nome, TipoMassa tipo)
    {
        Nome = nome;
        TipoMassa = tipo;
    }
}