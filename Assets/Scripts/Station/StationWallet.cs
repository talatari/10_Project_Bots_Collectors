using System;
using UnityEngine;

public class StationWallet : MonoBehaviour
{
    [SerializeField] private int _countResources;
    
    private StationResourceCollector _stationResourceCollector;
    private int _amountResourcesForCreateUnit = 3;
    private int _amountResourcesForCreateStation = 5;

    public int CountResources { get; set; } = 3;
    
    public event Action CountResourcesUpdate;
    public event Action EnoughResourcesForUnit;
    public event Action EnoughResourcesForStation;

    private void Awake() => 
        _stationResourceCollector = GetComponent<StationResourceCollector>();

    private void Start()
    {
        CanSpawnUnit();
        // CanSpawnStation();
        _countResources = CountResources;
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
        _countResources = CountResources;

        CountResourcesUpdate?.Invoke();
        
        CanSpawnUnit();
        //CanSpawnStation();
    }

    private void CanSpawnUnit()
    {
        if (HaveResourcesForCreateUnit())
        {
            EnoughResourcesForUnit?.Invoke();
        }
            
    }
    
    private void CanSpawnStation()
    {
        if (HaveResourcesForCreateStation())
            EnoughResourcesForStation?.Invoke();
    }
    
    private bool HaveResourcesForCreateUnit() => 
        CountResources - _amountResourcesForCreateUnit >= 0;
    
    private bool HaveResourcesForCreateStation() => 
        CountResources - _amountResourcesForCreateStation >= 0;
}