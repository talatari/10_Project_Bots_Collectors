using UnityEngine;

[RequireComponent(
    typeof(UnitMovement), 
    typeof(UnitCollector))]

public class Unit : MonoBehaviour
{
    private UnitMovement _movement;
    private UnitCollector _collector;
    private Transform _basePosition;
    
    public bool IsWork { get; private set; }
    public UnitCollector Collector => _collector;
    
    private void Awake()
    {
        _movement = GetComponent<UnitMovement>();
        _collector = GetComponent<UnitCollector>();
    }

    private void OnEnable() => 
        _collector.ResourceCollected += OnResourceCollected;

    private void OnDisable() => 
        _collector.ResourceCollected -= OnResourceCollected;

    private void OnResourceCollected()
    {
        _movement.SetTarget(_basePosition);
    }

    public void Destroy() => 
        Destroy(gameObject);

    public void AssignWork(Resource resource)
    {
        _movement.SetTarget(resource.transform);
        _collector.SetTargetResource(resource);
        IsWork = true;
    }

    public void SetFree()
    {
        IsWork = false;
        _collector.ClearResource();
        
        //TODO: event Unit Free for scanner
    }

    public void SetBasePosition(Transform baseTransform) => 
        _basePosition = baseTransform;

    
    
    
}