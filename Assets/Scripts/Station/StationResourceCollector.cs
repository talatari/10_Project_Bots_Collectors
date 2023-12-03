using System;
using UnityEngine;

public class StationResourceCollector : MonoBehaviour
{
    public event Action Collected = delegate { };

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            if (unit.UnitCollector.HasResource)
                Collect(unit);
    }

    private void Collect(Unit unit)
    {
        unit.UnitCollector.Resource.Destroy();
        unit.UnitCollector.ClearResource();
        unit.SetFree();
        
        Collected();
    }
}