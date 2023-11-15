using System.Collections.Generic;

public class CollectorCollection
{
    private List<Collector> _collectors = new();

    public void Add(Collector collector) => _collectors.Add(collector);

    public Collector TryGetFreeUnit()
    {
        foreach (Collector collector in _collectors)
            if (collector.IsWork == false)
                return collector;

        return null;
    }
    
    public int CountFree()
    {
        var countFree = 0;

        foreach (Collector collector in _collectors)
            if (collector.IsWork == false)
                countFree++;

        return countFree;
    }

    public void Clear()
    {
        foreach (Collector collector in _collectors) 
            collector.Destroy();

        _collectors.Clear();
    }
    
    
}