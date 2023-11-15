using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private CollectorMovement _collectorMovement;
    [SerializeField] private Base _base;

    private Mineral _mineral;
    
    public bool IsWork { get; private set; }
    
    private void OnValidate() => _collectorMovement ??= GetComponent<CollectorMovement>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Mineral mineral))
        {
            _mineral = mineral;
            _mineral.transform.SetParent(transform);
            _collectorMovement.SetTarget(_base.transform);
        }
    }

    public void Destroy() => Destroy(gameObject);

    public void AssignWork(Mineral mineral)
    {
        _collectorMovement.SetTarget(mineral.transform);
        IsWork = true;
    }

    public void SetFree() => IsWork = false;
    
    
}