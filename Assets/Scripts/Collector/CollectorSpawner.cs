using UnityEngine;

public class CollectorSpawner : MonoBehaviour
{
    [SerializeField] private Collector _collectorPrefab;
    [SerializeField] private int _countUnit = 3;

    private CollectorCollection _collectorCollection;
    
    public void Initialize(CollectorCollection collectorCollection) => _collectorCollection = collectorCollection;
    
    public void Spawn()
    {
        for (int i = 0; i < _countUnit; i++)
        {
            Collector collector = Instantiate(_collectorPrefab, Vector3.zero, Quaternion.identity);
            _collectorCollection.Add(collector);
            
            collector.transform.parent = gameObject.transform;
        }
    }
    
    
}