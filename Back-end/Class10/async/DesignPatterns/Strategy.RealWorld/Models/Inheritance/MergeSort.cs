using Strategy.RealWorld.Models.Abstract;

namespace Strategy.RealWorld.Models.Inheritance;

/// <summary>
/// A classe "Estratégia" concreta
/// </summary>
public class MergeSort : SortStrategy
{
    public override string GetDescription()
    {
        return "MergeSort: ordenação por divisão e conquista";
    }

    public override IEnumerable<string> Sort(List<string> list)
    {
        var algoritmo = new SmartSorting.Algorithms.MergeSort<string>();
        return algoritmo.Sort(list, SmartSorting.Structure.ESortOrder.Ascending);
    }
}