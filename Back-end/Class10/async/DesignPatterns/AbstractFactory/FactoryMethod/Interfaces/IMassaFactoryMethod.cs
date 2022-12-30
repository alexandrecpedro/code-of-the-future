using AbstractFactory.Models.Abstract;

namespace AbstractFactory.FactoryMethod.Interfaces;

public interface IMassaFactoryMethod
{
    MassaBase CriaMassa(Enum massaFactoryType);
}