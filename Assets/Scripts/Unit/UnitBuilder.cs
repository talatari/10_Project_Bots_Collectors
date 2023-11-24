using System.Collections;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{
    private Station _stationPrefab;
    private LevelHandler _levelHandler;
    private Unit _unit;
    private UnitMover _unitMover;
    private Coroutine _coroutineWaitUnitForBuild;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _unitMover = GetComponent<UnitMover>();
        _stationPrefab = FindObjectOfType<Station>();
        _levelHandler = FindObjectOfType<LevelHandler>();
    }

    private void OnDestroy()
    {
        if (_coroutineWaitUnitForBuild is not null)
            StopCoroutine(_coroutineWaitUnitForBuild);
    }

    public void SpawStation() => 
        Instantiate(_stationPrefab, _levelHandler.GetSpawnPoint(), Quaternion.identity);
    
    public void BuildStation(Vector3 buildPosition)
    {
        _unitMover.SetTarget(buildPosition);
        _coroutineWaitUnitForBuild = StartCoroutine(WaitUnitForBuild(buildPosition));
        _unit.SetBusy();
    }
    
    private IEnumerator WaitUnitForBuild(Vector3 buildPosition)
    {
        while (transform.position != buildPosition)
            yield return null;
    
        Station newStation = Instantiate(_stationPrefab, buildPosition, Quaternion.identity);
        gameObject.transform.parent = newStation.transform;
    

    }
}