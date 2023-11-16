using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _countMinerals;

    private ResourceCollection _resourceCollection;
    private Unit _unit;
    
    public void Initialize(ResourceCollection resourceCollection) => _resourceCollection = resourceCollection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Unit unit)) 
            _unit = unit;

        if (collider.gameObject.TryGetComponent(out Resource resource))
        {
            _resourceCollection.Remove(resource);
            
            _countMinerals++;

            if (_unit is not null)
            {
                _unit.SetFree();
                _unit.ClearResource();
            }
        }
    }
    
    
}