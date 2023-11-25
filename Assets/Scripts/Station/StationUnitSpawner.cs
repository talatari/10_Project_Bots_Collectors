using System;
using UnityEngine;

public class StationUnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    
    private Station _station;

    public event Action<Unit> SpawnedUnit; 

    public void SetParentStation(Station station) => 
        _station = station;

    public void Spaw()
    {
        Unit unit = Instantiate(_unitPrefab, _station.transform.position, Quaternion.identity);
        unit.transform.parent = gameObject.transform;
        unit.SetParentStation(_station);
        
        SpawnedUnit?.Invoke(unit);
    }
}