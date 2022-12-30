using Strategy.RealWorld.Models.Abstract;

namespace Strategy.RealWorld.Models;

/// <summary>
/// A classe de "contexto" (FURADEIRA)
/// </summary>
public class SortedList
{
    private List<string> list = new List<string>();
    private SortStrategy sortStrategy = default!;

    public void SetSortStrategy(SortStrategy sortStrategy)
    {
        this.sortStrategy = sortStrategy;
    }

    public void Add(string name)
    {
        list.Add(name);
    }

    public void Sort()
    {
        // list.Sort();
        Console.WriteLine(sortStrategy.GetDescription());
        var ordenado = sortStrategy.Sort(list);

        // Percorrer a lista e exibir os resultados
        foreach (string nome in ordenado)
        {
            Console.WriteLine($" {nome}");
        }
        Console.WriteLine();
    }
}