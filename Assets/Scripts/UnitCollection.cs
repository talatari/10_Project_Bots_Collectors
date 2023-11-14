using System.Collections.Generic;

public class UnitCollection
{
    private List<Unit> _units = new();

    public void Add(Unit unit) => _units.Add(unit);

    public void Clear()
    {
        foreach (Unit unit in _units)
        {
            unit.Clear();
        }
        
        _units.Clear();
    }
    
    
}