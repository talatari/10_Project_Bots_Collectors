using System;
using UnityEngine;

public class StationUnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    
    private Vector3 _stationPosition;

    public event Action<Unit> SpawnedUnit; 

    public void SetStationPosition(Vector3 position) => 
        _stationPosition = position;

    public void Spaw()
    {
        Unit unit = Instantiate(_unitPrefab, _stationPosition, Quaternion.identity);
        unit.transform.parent = gameObject.transform;
        unit.SetStationPosition(_stationPosition);
        
        SpawnedUnit?.Invoke(unit);
    }
}