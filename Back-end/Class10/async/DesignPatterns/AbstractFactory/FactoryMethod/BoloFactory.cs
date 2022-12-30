using AbstractFactory.FactoryMethod.Interfaces;
using AbstractFactory.Models;
using AbstractFactory.Models.Abstract;
using AbstractFactory.Models.Enum;

namespace AbstractFactory.FactoryMethod;

public sealed class BoloFactory : IMassaFactoryMethod
{
    public MassaBase CriaMassa(Enum massaFactoryType)
    {
        var tipoBolo = (TipoBolo)massaFactoryType;

        switch (tipoBolo)
        {
            case TipoBolo.Chocolate:
            {
                return new BoloChocolate();
            }
            case TipoBolo.Laranja:
            {
                    return new BoloLaranja();
            }
            default:
                throw new ArgumentOutOfRangeException("Tipo não implementado");
        }
    }
}