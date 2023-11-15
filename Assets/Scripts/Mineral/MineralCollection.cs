using System.Collections.Generic;
using UnityEngine;

public class MineralCollection
{
    private List<Mineral> _minerals = new ();

    public int Count => _minerals.Count;
    
    public void Add(Mineral mineral) => _minerals.Add(mineral);

    public Mineral TryGetPositionMineral()
    {
        if (_minerals.Count > 0)
        {
            int randomMineralIndex = Random.Range(0, _minerals.Count);

            return _minerals[randomMineralIndex];
        }
        
        return null;
    }
    
    public void Clear()
    {
        foreach (Mineral mineral in _minerals)
        {
            mineral.Clear();
        }
        
        _minerals.Clear();
    }
    
    
}