using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private int _countResources;

    public event Action UnitCollectorFree;
    
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
            _countResources++;
            unit.SetFree();
        }
    }
}