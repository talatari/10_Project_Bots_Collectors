using System.Collections.Generic;

public class UnitCollection
{
    private List<StationUnits> _stationUnits = new();
    
    public void Add(Unit unit, Station station) => 
        _stationUnits.Add(new StationUnits(unit, station));

    public Unit TryGetFreeUnit()
    {
        foreach (StationUnits _unit in _stationUnits)
        {
            Unit unit = _unit.GetUnit();
            
            if (unit.IsWork is false)
                return unit;
        }

        return null;
    }

    public void Clear()
    {
        foreach (StationUnits _unit in _stationUnits) 
            _unit.GetUnit().Destroy();

        _stationUnits.Clear();
    }
}

public class StationUnits
{
    private Unit _unit;
    private Station _station;

    public StationUnits(Unit unit, Station station)
    {
        _unit = unit;
        _station = station;
    }

    public Unit GetUnit() => _unit;

    public Station GetStation() => _station;
}