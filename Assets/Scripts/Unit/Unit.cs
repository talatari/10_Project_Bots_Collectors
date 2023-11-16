using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMovement _unitMovement;

    private Resource _resource;
    
    public Transform BasePosition { get; private set; }
    public bool IsWork { get; private set; }
    
    private void OnValidate() => _unitMovement ??= GetComponent<UnitMovement>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Resource resource))
        {
            _resource = resource;
            _resource.transform.SetParent(transform);
            _unitMovement.SetTarget(BasePosition);
        }
    }

    public void Destroy() => Destroy(gameObject);

    public void AssignWork(Resource resource)
    {
        _unitMovement.SetTarget(resource.transform);
        IsWork = true;
    }

    public void SetFree() => IsWork = false;

    public void SetBasePosition(Transform baseTransform) => BasePosition = baseTransform;
    
    
}