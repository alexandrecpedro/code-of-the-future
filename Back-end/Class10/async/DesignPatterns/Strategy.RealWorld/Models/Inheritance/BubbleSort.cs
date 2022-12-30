using Strategy.RealWorld.Models.Abstract;

namespace Strategy.RealWorld.Models.Inheritance;

/// <summary>
/// A classe "Estratégia" concreta
/// </summary>
public class BubbleSort : SortStrategy
{
    public override string GetDescription()
    {
        return "BubbleSort: ordenação por bolha";
    }

    public override IEnumerable<string> Sort(List<string> list)
    {
        var algoritmo = new SmartSorting.Algorithms.BubbleSort<string>();
        return algoritmo.Sort(list, SmartSorting.Structure.ESortOrder.Ascending);
    }
}