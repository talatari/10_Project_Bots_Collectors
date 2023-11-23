using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private StationSpawner _stationSpawner;
    
    private int _amountResourcesForCreateUnit = 3;
    private int _amountResourcesForCreateStation = 5;
    private bool _isModeSelectParentStation;
    private Vector3 _stationPosition;
    private Vector3 _stationSpawnPosition;

    public int CountResources { get; set; }

    public event Action UnitCollectorFree;
    public event Action CountResourcesUpdate;
    public event Action EnoughResourcesForUnit;
    public event Action NotEnoughResourcesForUnit;
    public event Action EnoughResourcesForStation;
    public event Action NotEnoughResourcesForStation;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.UnitCollector.HasResource == false)
            {
                UnitCollectorFree?.Invoke();
                return;
            }
            
            unit.UnitCollector.Resource.Destroy();
            unit.SetFree();
            
            IncreaseCountResources();
            
            CanCreateUnit();
            CanCreateStation();

            if (_stationSpawner.HaveSpawnPointStation)
            {
                unit.BuildStation(_stationSpawner.GetSpawnPoint());
            }
        }
    }
    
    public void OnMouseDown()
    {
        if (_isModeSelectParentStation)
        {
            _stationPosition = transform.position;
            _isModeSelectParentStation = false;
            _stationSpawner.IsModeSelectSpawnPointStation = true;
            
            print($"_stationPosition = {_stationPosition}");
        }
    }

    public void CreateUnitHadle()
    {
        if (HaveResourcesForCreateUnit())
        {
            CountResources -= _amountResourcesForCreateUnit;
            CountResourcesUpdate?.Invoke();

            CreateUnit();
        }
        
        CanCreateUnit();
        CanCreateStation();
    }
    
    public void CreateStationHadle()
    {
        if (HaveResourcesForCreateStation())
        {
            CountResources -= _amountResourcesForCreateStation;
            CountResourcesUpdate?.Invoke();

            CreateStation();
        }
        
        CanCreateUnit();
        CanCreateStation();
    }

    private void IncreaseCountResources()
    {
        CountResources++;
        CountResourcesUpdate?.Invoke();
    }

    private void CanCreateUnit()
    {
        if (HaveResourcesForCreateUnit())
            EnoughResourcesForUnit?.Invoke();
        else
            NotEnoughResourcesForUnit?.Invoke();
    }
    
    private void CanCreateStation()
    {
        if (HaveResourcesForCreateStation())
            EnoughResourcesForStation?.Invoke();
        else
            NotEnoughResourcesForStation?.Invoke();
    }

    private bool HaveResourcesForCreateUnit() => 
        CountResources - _amountResourcesForCreateUnit >= 0;
    
    private bool HaveResourcesForCreateStation() => 
        CountResources - _amountResourcesForCreateStation >= 0;

    private void CreateUnit() =>
        _unitSpawner.SpawnUnit();

    private void CreateStation() => 
        _isModeSelectParentStation = true;
}