using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private int _countResources;

    private ResourceCollection _resourceCollection;
    
    public event Action UnitCollectorFree;
    
    public void Initialize(ResourceCollection resourceCollection) => 
        _resourceCollection = resourceCollection;

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