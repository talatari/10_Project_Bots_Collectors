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
            if (_unit.Unit.IsWork is false)
                return _unit.Unit;
        }

        return null;
    }

    public void AssginUnit(Station station, Unit unit)
    {
        foreach (StationUnits _unit in _stationUnits)
        {
            if (_unit.Unit == unit)
            {
                _unit.Station = station;
                unit.SetFree();
            }
        }
    }

    public void Clear()
    {
        foreach (StationUnits _unit in _stationUnits) 
            _unit.Unit.Destroy();

        _stationUnits.Clear();
    }
}

public class StationUnits
{
    public Unit Unit;
    public Station Station;

    public StationUnits(Unit unit, Station station)
    {
        Unit = unit;
        Station = station;
    }
}