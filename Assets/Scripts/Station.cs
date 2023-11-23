using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private int _amountResourcesForCreateStation = 3;
    
    public int CountResources { get; set; }

    public event Action UnitCollectorFree;
    public event Action CountResourcesUpdate;
    public event Action EnoughResources;
    public event Action NotEnoughResources;
    
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
            
            CanCreateStation();
        }
    }

    public void CreateStationHadle()
    {
        if (HaveResources())
        {
            CountResources -= _amountResourcesForCreateStation;
            CountResourcesUpdate?.Invoke();
        }
        
        CanCreateStation();
    }

    private void IncreaseCountResources()
    {
        CountResources++;
        CountResourcesUpdate?.Invoke();
    }

    private void CanCreateStation()
    {
        if (HaveResources())
            EnoughResources?.Invoke();
        else
            NotEnoughResources?.Invoke();
    }

    private bool HaveResources() => 
        CountResources - _amountResourcesForCreateStation >= 0;
}