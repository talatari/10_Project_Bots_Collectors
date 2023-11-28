using System;
using UnityEngine;

public class StationWallet : MonoBehaviour
{
    [SerializeField] private int _countResources;
    
    private StationResourceCollector _stationResourceCollector;
    private int _amountResourcesForCreateUnit = 3;
    private int _amountResourcesForCreateStation = 5;

    public int CountResources { get; set; }
    
    public event Action EnoughResourcesForUnit;
    public event Action EnoughResourcesForStation;

    private void Awake() => 
        _stationResourceCollector = GetComponent<StationResourceCollector>();

    private void Start()
    {
        CountResources = _countResources;
        CanSpawnUnit();
    } 

    private void OnEnable() => 
        _stationResourceCollector.StationResourceCollected += OnIncreaseCountStationResources;

    private void OnDisable() => 
        _stationResourceCollector.StationResourceCollected -= OnIncreaseCountStationResources;

    public void DecreaseResourcesForUnit()
    {
        CountResources -= _amountResourcesForCreateUnit;
        _countResources = CountResources;
    }
    
    public void DecreaseResourcesForStation()
    {
        CountResources -= _amountResourcesForCreateStation;
        _countResources = CountResources;
    }

    private void OnIncreaseCountStationResources()
    {
        CountResources++;
        _countResources = CountResources;
        
        CanSpawnStation();
        CanSpawnUnit();
    }

    public void CanSpawnUnit()
    {
        if (HaveResourcesForCreateUnit())
            EnoughResourcesForUnit?.Invoke();
    }
    
    public void CanSpawnStation()
    {
        if (HaveResourcesForCreateStation())
            EnoughResourcesForStation?.Invoke();
    }
    
    private bool HaveResourcesForCreateUnit() => 
        CountResources - _amountResourcesForCreateUnit >= 0;
    
    private bool HaveResourcesForCreateStation() => 
        CountResources - _amountResourcesForCreateStation >= 0;
}