using System.Collections.Generic;

public class MineralCollection
{
    private List<Mineral> _mineralsList = new ();

    public void Add(Mineral mineral) => _mineralsList.Add(mineral);

    public void Clear()
    {
        foreach (Mineral mineral in _mineralsList)
        {
            mineral.Clear();
        }
        
        _mineralsList.Clear();
    }
    
    
}