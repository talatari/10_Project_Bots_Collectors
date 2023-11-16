using UnityEngine;

[RequireComponent(
    typeof(UnitMover), 
    typeof(UnitCollector))]

public class Unit : MonoBehaviour
{
    private UnitMover _unitMover;
    private UnitCollector _unitCollector;
    private Transform _basePosition;
    
    public bool IsWork { get; private set; }
    public UnitCollector UnitCollector => _unitCollector;
    
    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitCollector = GetComponent<UnitCollector>();
    }

    private void OnEnable() => 
        _unitCollector.ResourceCollected += OnResourceCollected;

    private void OnDisable() => 
        _unitCollector.ResourceCollected -= OnResourceCollected;

    private void OnResourceCollected() => 
        _unitMover.SetTarget(_basePosition);

    public void Destroy() => 
        Destroy(gameObject);

    public void AssignWork(Resource resource)
    {
        _unitMover.SetTarget(resource.transform);
        _unitCollector.SetTargetResource(resource);
        IsWork = true;
    }

    public void SetFree()
    {
        IsWork = false;
        _unitCollector.ClearResource();
    }

    public void SetBasePosition(Transform baseTransform) => 
        _basePosition = baseTransform;
    
    
}