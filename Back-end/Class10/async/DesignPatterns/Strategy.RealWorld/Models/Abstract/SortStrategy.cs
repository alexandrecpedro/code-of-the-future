namespace Strategy.RealWorld.Models.Abstract;

/// <summary>
/// A classe abstrata "Estratégia"
/// </summary>
public abstract class SortStrategy
{
    public abstract string GetDescription();
    public abstract IEnumerable<string> Sort(List<string> list);
}