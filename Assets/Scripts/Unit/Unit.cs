using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitMover _unitMover;
    private UnitCollector _unitCollector;
    private UnitBuilder _unitBuilder;
    private Station _parentStation;

    public event Action<Unit> UnitFree;
    
    public UnitCollector UnitCollector => _unitCollector;
    
    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitCollector = GetComponent<UnitCollector>();
        _unitBuilder = GetComponent<UnitBuilder>();
    }

    private void OnEnable()
    {
        _unitCollector.UnitResourceCollected += OnUnitResourceCollected;
        _unitBuilder.SpawnedStation += OnReConnectStation;
    }

    private void OnDisable()
    {
        _unitCollector.UnitResourceCollected -= OnUnitResourceCollected;
        _unitBuilder.SpawnedStation -= OnReConnectStation;
    }

    public void CollectResource(Resource resource)
    {
        _unitMover.SetTarget(resource.transform.position);
        _unitCollector.SetTargetResource(resource);
    }

    public void SetFree() => 
        UnitFree?.Invoke(this);

    public void SetParentStation(Station station) => 
        _parentStation = station;

    private void OnUnitResourceCollected() => 
        _unitMover.SetTarget(_parentStation.transform.position);

    private void OnReConnectStation(Station newStation, Unit unit)
    {
        _parentStation.RemoveUnit(unit);
        newStation.OnAddUnit(unit);
        _parentStation = newStation;
    }
}