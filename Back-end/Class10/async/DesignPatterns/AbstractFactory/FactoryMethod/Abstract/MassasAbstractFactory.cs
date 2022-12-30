using AbstractFactory.FactoryMethod.Interfaces;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.FactoryMethod.Abstract;

public abstract class MassasAbstractFactory
{
    public static IMassaFactoryMethod CriaFabrica(TipoMassa tipoMassa)
    {
        switch (tipoMassa)
        {
            case TipoMassa.Pizza:
            {
                return new PizzaFactory();
            }
            case TipoMassa.Bolo:
            {
                return new BoloFactory();
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(tipoMassa),
                    tipoMassa, null);
        }
    }
}