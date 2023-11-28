using System;
using System.Collections;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{
    [SerializeField] private Station _stationPrefab;
    
    private Unit _unit;
    private UnitMover _unitMover;
    private Coroutine _coroutineWaitUnitForBuild;

    public event Action<Station, Unit> SpawnedStation; 

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitMover = GetComponent<UnitMover>();
    }

    private void OnDestroy()
    {
        if (_coroutineWaitUnitForBuild is not null)
            StopCoroutine(_coroutineWaitUnitForBuild);
    }
    
    public void BuildStation(Vector3 buildPosition)
    {
        _unitMover.SetTarget(buildPosition);
        _coroutineWaitUnitForBuild = StartCoroutine(WaitUnitForBuild(buildPosition));
    }
    
    private IEnumerator WaitUnitForBuild(Vector3 buildPosition)
    {
        while (transform.position != buildPosition)
            yield return null;
        
        Station newStation = Instantiate(_stationPrefab, buildPosition, Quaternion.identity);
        transform.parent = newStation.transform;
        
        SpawnedStation?.Invoke(newStation, _unit);
        
        // _unit.SetFree();
    }
}