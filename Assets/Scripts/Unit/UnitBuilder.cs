using System.Collections;
using UnityEngine;

public class UnitBuilder : MonoBehaviour
{
    private UnitMover _unitMover;
    private Station _stationPrefab;
    private Coroutine _coroutineWaitUnitForBuild;

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _stationPrefab = FindObjectOfType<Station>();
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
        gameObject.transform.parent = newStation.transform;
    }
}