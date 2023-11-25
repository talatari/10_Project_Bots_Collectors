using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitMover _unitMover;
    private UnitCollector _unitCollector;
    private UnitBuilder _unitBuilder;
    private Station _station;

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
        _unitBuilder.SpawnedStation += OnReConnectStation;
    }

    private void OnDisable()
    {
        _unitCollector.ResourceCollected -= OnResourceCollected;
        _unitBuilder.SpawnedStation -= OnReConnectStation;
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

    public void SetParentStation(Station station) => 
        _station = station;

    private void OnResourceCollected() => 
        _unitMover.SetTarget(_station.transform.position);

    private void OnReConnectStation(Station newStation, Unit unit)
    {
        _station.RemoveUnit(unit);
        newStation.OnAddUnit(unit);
        _station = newStation;
    }
}