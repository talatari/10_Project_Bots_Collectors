using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitMover _unitMover;
    private UnitCollector _unitCollector;
    private UnitBuilder _unitBuilder;
    private Vector3 _stationPosition;

    public bool IsWork { get; private set; }
    public UnitCollector UnitCollector => _unitCollector;
    
    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitCollector = GetComponent<UnitCollector>();
        _unitBuilder = GetComponent<UnitBuilder>();
    }

    private void OnEnable()
    {
        _unitCollector.ResourceCollected += OnResourceCollected;
    }

    private void OnDisable()
    {
        _unitCollector.ResourceCollected -= OnResourceCollected;
    }

    public void CollectResource(Resource resource)
    {
        _unitMover.SetTarget(resource.transform.position);
        _unitCollector.SetTargetResource(resource);
        IsWork = true;
    }

    public void SpawnStation(Vector3 buildPosition)
    {
        _unitBuilder.BuildStation(buildPosition);
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
}