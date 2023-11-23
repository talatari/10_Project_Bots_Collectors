using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    
    public int CountUnit = 3;
    
    private UnitCollection _unitCollection;
    private Station _station;

    public void Initialize(UnitCollection unitCollection, Station station)
    {
        _unitCollection = unitCollection;
        _station = station;
    }

    public void SpawnUnit()
    {
        Unit unit = Instantiate(_unitPrefab, _station.transform.position, Quaternion.identity);
        _unitCollection.Add(unit, _station);

        unit.transform.parent = gameObject.transform;
        unit.SetStationPosition(_station.transform.position);
    }

    public void AssginUnit(Station station, Unit unit)
    {
        _unitCollection.AssginUnit(station, unit);
    }
}