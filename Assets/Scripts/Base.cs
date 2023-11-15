using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _countMinerals;

    private MineralCollection _mineralCollection;
    private CollectorCollection _collectorCollection;

    private CollectorMovement _collectorMovement;
    
    public void Initialize(MineralCollection mineralCollection, CollectorCollection collectorCollection)
    {
        _mineralCollection = mineralCollection;
        _collectorCollection = collectorCollection;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Mineral mineral))
        {
            _mineralCollection.Remove(mineral);
            
            _countMinerals++;
            
            if (_collectorMovement != null) 
                _collectorMovement.ClearTarget();
            
            _collectorMovement = null;
        }
        
        if (collider.gameObject.TryGetComponent(out Collector collector))
        {
            if (collector.TryGetComponent(out CollectorMovement collectorMovement))
                _collectorMovement = collectorMovement;
        }
    }
    
    
}