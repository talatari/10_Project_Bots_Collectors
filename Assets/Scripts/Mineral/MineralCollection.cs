using System.Collections.Generic;

public class MineralCollection
{
    private List<Mineral> _minerals = new ();

    public int CountFree()
    {
        int countFree = 0;

        foreach (Mineral mineral in _minerals)
            if (mineral.IsBusy == false)
                countFree++;

        return countFree;
    }

    public void Add(Mineral mineral) => _minerals.Add(mineral);

    public Mineral TryGetPositionMineral()
    {
        foreach (Mineral mineral in _minerals)
            if (mineral.IsBusy == false)
                return mineral;

        return null;
    }

    public void Clear()
    {
        foreach (Mineral mineral in _minerals) 
            mineral.Clear();

        _minerals.Clear();
    }
    
    
}