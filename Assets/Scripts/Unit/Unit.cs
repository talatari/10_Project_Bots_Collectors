using System.Collections;
using UnityEngine;

[RequireComponent(
    typeof(UnitMover), 
    typeof(UnitCollector))]

public class Unit : MonoBehaviour
{
    [SerializeField] private Station _stationPrefab;
    
    private UnitMover _unitMover;
    private UnitCollector _unitCollector;
    private Vector3 _stationPosition;
    private Coroutine _coroutineWaitUnitForBuild;
    
    public bool IsWork { get; private set; }
    public UnitCollector UnitCollector => _unitCollector;
    
    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitCollector = GetComponent<UnitCollector>();
    }

    private void OnEnable() => 
        _unitCollector.ResourceCollected += OnResourceCollected;

    private void OnDisable() => 
        _unitCollector.ResourceCollected -= OnResourceCollected;

    private void OnDestroy()
    {
        if (_coroutineWaitUnitForBuild is not null)
            StopCoroutine(_coroutineWaitUnitForBuild);
    }

    public void Destroy() => 
        Destroy(gameObject);

    public void CollectResource(Resource resource)
    {
        _unitMover.SetTarget(resource.transform.position);
        _unitCollector.SetTargetResource(resource);
        IsWork = true;
    }

    public void BuildStation(Vector3 buildPosition)
    {
        _unitMover.SetTarget(buildPosition);
        _coroutineWaitUnitForBuild = StartCoroutine(WaitUnitForBuild(buildPosition));
        IsWork = true;
    }

    public void SetFree()
    {
        IsWork = false;
        _unitCollector.ClearResource();
    }

    public void SetStationPosition(Vector3 stationPosition) => 
        _stationPosition = stationPosition;

    private void OnResourceCollected() => 
        _unitMover.SetTarget(_stationPosition);

    private IEnumerator WaitUnitForBuild(Vector3 buildPosition)
    {
        while (transform.position != buildPosition)
        {
            yield return null;
        }

        Station newStation = Instantiate(_stationPrefab, buildPosition, Quaternion.identity);
        gameObject.transform.parent = newStation.transform;

        if (newStation.TryGetComponent(out UnitSpawner unitSpawner))
        {
            unitSpawner.AssginUnit(newStation, this);
        }
    }
}