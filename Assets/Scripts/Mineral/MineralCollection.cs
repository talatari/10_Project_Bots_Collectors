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

    public void RemoveAll()
    {
        foreach (Mineral mineral in _minerals) 
            mineral.Destroy();

        _minerals.Clear();
    }

    public void Remove(Mineral mineral)
    {
        _minerals.Remove(mineral);
        
        mineral.Destroy();
    }
}