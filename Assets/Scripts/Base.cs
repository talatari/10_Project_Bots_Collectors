using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _countResources;

    private ResourceCollection _resourceCollection;
    
    public void Initialize(ResourceCollection resourceCollection) => _resourceCollection = resourceCollection;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.TryGetComponent(out Unit unit))
        {
            if (unit.Collector.HasResource is false)
                return;
            
            _resourceCollection.Remove(unit.Collector.Resource);
            _countResources++;
            unit.SetFree();
        }
    }
    
    
}