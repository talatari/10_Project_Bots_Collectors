using System.Collections.Generic;

public class UnitCollection
{
    private List<Unit> _units = new();

    public void Add(Unit unit) => _units.Add(unit);

    public Unit TryGetFreeUnit()
    {
        foreach (Unit unit in _units)
            if (unit.IsWork == false)
                return unit;

        return null;
    }
    
    public int CountFree()
    {
        var countFree = 0;

        foreach (Unit unit in _units)
            if (unit.IsWork == false)
                countFree++;

        return countFree;
    }

    public void Clear()
    {
        foreach (Unit unit in _units) 
            unit.Destroy();

        _units.Clear();
    }
    
    
}