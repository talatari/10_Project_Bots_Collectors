using System;
using UnityEngine;

public class StationUnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    
    private Station _station;

    public event Action<Unit> Spawned = delegate { }; 

    public void SetParentStation(Station station) => 
        _station = station;

    public void Spaw()
    {
        string unitNamePrefix = "Unit";
        
        Unit unit = Instantiate(_unitPrefab, _station.transform.position, Quaternion.identity);
        unit.name = unitNamePrefix + unit.GetInstanceID();
        unit.transform.parent = gameObject.transform;
        unit.SetParentStation(_station);
        
        Spawned(unit);
    }
}