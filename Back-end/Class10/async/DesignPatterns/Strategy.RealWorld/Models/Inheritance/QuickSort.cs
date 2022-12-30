using Strategy.RealWorld.Models.Abstract;

namespace Strategy.RealWorld.Models.Inheritance;

/// <summary>
/// A classe "Estratégia" concreta
/// </summary>
public class QuickSort : SortStrategy
{
    public override string GetDescription()
    {
        return "QuickSort: execuções de particionamento";
    }

    public override IEnumerable<string> Sort(List<string> list)
    {
        var algoritmo = new SmartSorting.Algorithms.QuickSort<string>();
        return algoritmo.Sort(list, SmartSorting.Structure.ESortOrder.Ascending);
    }
}