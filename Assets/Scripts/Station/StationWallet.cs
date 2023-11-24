using System;
using UnityEngine;

public class StationWallet : MonoBehaviour
{
    [SerializeField] private StationCollector _stationCollector;
    
    private int _amountResourcesForCreateUnit = 3;
    private int _amountResourcesForCreateStation = 5;

    public int CountResources { get; set; } = 3;
    
    public event Action CountResourcesUpdate;
    public event Action SpawnUnit;
    
    public event Action EnoughResourcesForUnit;
    public event Action NotEnoughResourcesForUnit;
    
    public event Action EnoughResourcesForStation;
    public event Action NotEnoughResourcesForStation;

    private void Start()
    {
        CanSpawnUnit();
    }

    private void OnEnable()
    {
        _stationCollector.CollectResource += OnIncreaseCountResources;
    }

    private void OnDisable()
    {
        _stationCollector.CollectResource -= OnIncreaseCountResources;
    }

    public void SpawnUnitHadle()
    {
        if (HaveResourcesForCreateUnit())
        {
            CountResources -= _amountResourcesForCreateUnit;
            CountResourcesUpdate?.Invoke();
            SpawnUnit?.Invoke();
        }
        
        CanSpawnUnit();
        CanSpawnStation();
    }
    
    public void CreateStationHadle()
    {
        if (HaveResourcesForCreateStation())
        {
            CountResources -= _amountResourcesForCreateStation;
            CountResourcesUpdate?.Invoke();
        }
        
        CanSpawnUnit();
        CanSpawnStation();
    }

    private void OnIncreaseCountResources()
    {
        CountResources++;
        CountResourcesUpdate?.Invoke();
        
        CanSpawnUnit();
        CanSpawnStation();
    }

    private void CanSpawnUnit()
    {
        if (HaveResourcesForCreateUnit())
            EnoughResourcesForUnit?.Invoke();
        else
            NotEnoughResourcesForUnit?.Invoke();
    }
    
    private void CanSpawnStation()
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
}