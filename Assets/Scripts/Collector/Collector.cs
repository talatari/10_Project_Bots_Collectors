using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private CollectorMovement _collectorMovement;

    private void OnValidate() => _collectorMovement ??= GetComponent<CollectorMovement>();

    public bool IsWork { get; private set; }
    
    public void Clear() => Destroy(gameObject);

    public void AssignWork(Vector3 target)
    {
        _collectorMovement.Set(target);
        IsWork = true;
    }
    
}