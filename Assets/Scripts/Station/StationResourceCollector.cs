using System;
using UnityEngine;

public class StationResourceCollector : MonoBehaviour
{
    public event Action ResourceCollected;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            if (unit.UnitCollector.HasResource)
                Collect(unit);
    }

    private void Collect(Unit unit)
    {
        unit.UnitCollector.Resource.Destroy();
        unit.SetFree();
        
        ResourceCollected?.Invoke();
    }
}