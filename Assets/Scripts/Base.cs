using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _countMinerals;

    private ResourceCollection _resourceCollection;
    private UnitMovement _unitMovement;
    
    public void Initialize(ResourceCollection resourceCollection) => _resourceCollection = resourceCollection;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Resource resource))
        {
            _resourceCollection.Remove(resource);
            
            _countMinerals++;
            
            if (_unitMovement != null) 
                _unitMovement.ClearTarget();
            
            _unitMovement = null;
        }
        
        if (collider.gameObject.TryGetComponent(out Unit unit))
        {
            if (unit.TryGetComponent(out UnitMovement collectorMovement))
                _unitMovement = collectorMovement;
        }
    }
    
    
}