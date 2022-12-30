using AbstractFactory.FactoryMethod.Abstract;
using AbstractFactory.Models.Abstract;
using AbstractFactory.Models.Enum;

namespace AbstractFactory;

public class Program
{
    public static void Main(string[] args)
    {
        //Obtém as fábricas
        var boloFactory = MassasAbstractFactory.CriaFabrica(TipoMassa.Bolo);
        var pizzaFactory = MassasAbstractFactory.CriaFabrica(TipoMassa.Pizza);

        //cria os objetos com base no tipo : bolo
        var bolo1 = boloFactory.CriaMassa(TipoBolo.Chocolate);
        var bolo2 = boloFactory.CriaMassa(TipoBolo.Laranja);

        //cria os objetos com base no tipo : pizza
        var pizza1 = pizzaFactory.CriaMassa(TipoPizza.Mussarela);
        var pizza2 = pizzaFactory.CriaMassa(TipoPizza.Calabreza);

        //exibe os detalhes
        ExibeDetalhes(bolo1);
        ExibeDetalhes(bolo2);
        ExibeDetalhes(pizza1);
        ExibeDetalhes(pizza2);

        Console.ReadLine();
    }

    private static void ExibeDetalhes(MassaBase massaBase)
    {
        Console.WriteLine($"Tipo : {massaBase.TipoMassa}");
        Console.WriteLine(massaBase.Nome);
        Console.WriteLine(massaBase?.Ingredientes[0]?.ToString());
        Console.WriteLine("\n");
    }
}