using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Station : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int _amountResourcesForCreateUnit = 3;
    [SerializeField] private UnitSpawner _unitSpawner;
    
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
            
            CanCreateUnit();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        print("Tap station");
    }

    public void CreateStationHadle()
    {
        if (HaveResources())
        {
            CountResources -= _amountResourcesForCreateUnit;
            CountResourcesUpdate?.Invoke();

            CreateUnit();
        }
        
        CanCreateUnit();
    }

    private void IncreaseCountResources()
    {
        CountResources++;
        CountResourcesUpdate?.Invoke();
    }

    private void CanCreateUnit()
    {
        if (HaveResources())
            EnoughResources?.Invoke();
        else
            NotEnoughResources?.Invoke();
    }

    private bool HaveResources() => 
        CountResources - _amountResourcesForCreateUnit >= 0;

    private void CreateUnit() =>
        _unitSpawner.SpawnUnit();
}