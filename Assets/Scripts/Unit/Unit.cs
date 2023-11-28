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
        if (resource is not null)
        {
            resource.Units++;
            _unitMover.SetTarget(resource.transform.position);
            _unitCollector.SetTargetResource(resource);
        }
    }

    public void SetFree()
    {
        UnitFree?.Invoke(this);
        _unitCollector.ClearResource();
    }

    public void SetParentStation(Station station) => 
        _parentStation = station;

    private void OnResourceCollected() => 
        _unitMover.SetTarget(_parentStation.transform.position);

    private void OnReConnectStation(Station newStation, Unit unit)
    {
        _parentStation.RemoveUnit(unit);
        newStation.OnAddUnit(unit);
        _parentStation = newStation;
    }
}