using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private int _amountResourcesForCreateStation = 3;
    
    public int CountResources { get; set; }

    public event Action UnitCollectorFree;
    public event Action CountResourcesUpdate;
    public event Action EnoughResources; 
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.TryGetComponent(out Unit unit))
        {
            if (unit.UnitCollector.HasResource is false)
            {
                UnitCollectorFree?.Invoke();
                return;
            }
            
            unit.UnitCollector.Resource.Destroy();
            unit.SetFree();
            
            IncreaseCountResources();
        }
    }

    private void IncreaseCountResources()
    {
        CountResources++;
        CountResourcesUpdate?.Invoke();

        if (CountResources >= _amountResourcesForCreateStation)
            EnoughResources?.Invoke();
    }

    public void CreateStationHadle()
    {
        print(CountResources);
        CountResources -= _amountResourcesForCreateStation;
        CountResourcesUpdate?.Invoke();
    }
}