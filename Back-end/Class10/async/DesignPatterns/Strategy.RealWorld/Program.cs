using Strategy.RealWorld.Models;
using Strategy.RealWorld.Models.Inheritance;

namespace Strategy.RealWorld;

/// <summary>
/// Padrão de Design Estratégia
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        // Dois contextos seguindo estratégias diferentes

        SortedList sortedList = new SortedList();

        sortedList.Add("Saulo");
        sortedList.Add("Janaelton");
        sortedList.Add("Luan");
        sortedList.Add("Adriano");
        sortedList.Add("Aline");

        sortedList.SetSortStrategy(new QuickSort());
        sortedList.Sort();

        sortedList.SetSortStrategy(new BubbleSort());
        sortedList.Sort();

        sortedList.SetSortStrategy(new MergeSort());
        sortedList.Sort();

        // Aguarde o usuário
        Console.ReadKey();
    }
}