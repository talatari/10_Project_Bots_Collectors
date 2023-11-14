using System.Collections.Generic;

public class MineralCollection
{
    private List<Mineral> _minerals = new ();

    public void Add(Mineral mineral) => _minerals.Add(mineral);

    public void Clear()
    {
        foreach (Mineral mineral in _minerals)
        {
            mineral.Clear();
        }
        
        _minerals.Clear();
    }
    
    
}