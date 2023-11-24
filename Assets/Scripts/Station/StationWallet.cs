using System;
using UnityEngine;

public class StationWallet : MonoBehaviour
{
    private StationResourceCollector _stationResourceCollector;
    private int _amountResourcesForCreateUnit = 3;
    private int _amountResourcesForCreateStation = 5;

    public int CountResources { get; set; } = 11;
    
    public event Action CountResourcesUpdate;
    
    public event Action EnoughResourcesForUnit;
    public event Action NotEnoughResourcesForUnit;
    
    public event Action EnoughResourcesForStation;
    public event Action NotEnoughResourcesForStation;

    private void Awake() => 
        _stationResourceCollector = GetComponent<StationResourceCollector>();

    private void Start()
    {
        CanSpawnUnit();
        CanSpawnStation();
    } 

    private void OnEnable() => 
        _stationResourceCollector.ResourceCollected += OnIncreaseCountStationResources;

    private void OnDisable() => 
        _stationResourceCollector.ResourceCollected -= OnIncreaseCountStationResources;

    public void DecreaseResourcesForUnit()
    {
        if (HaveResourcesForCreateUnit())
        {
            CountResources -= _amountResourcesForCreateUnit;
            CountResourcesUpdate?.Invoke();
        }
        
        CanSpawnUnit();
        CanSpawnStation();
    }
    
    public void DecreaseResourcesForStation()
    {
        if (HaveResourcesForCreateStation())
        {
            CountResources -= _amountResourcesForCreateStation;
            CountResourcesUpdate?.Invoke();
        }
        
        CanSpawnUnit();
        CanSpawnStation();
    }

    private void OnIncreaseCountStationResources()
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