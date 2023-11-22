using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    public int CountResources { get; set; }

    public event Action UnitCollectorFree;
    public event Action CountResourcesUpdate;
    
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
            
            CountResources++;
            CountResourcesUpdate?.Invoke();
        }
    }
}