using System;
using UnityEngine;

public class UnitCollector: MonoBehaviour
{
    public event Action UnitResourceCollected;
    
    public bool HasResource { get; private set; }
    public Resource Resource { get; private set; }
    
    private void Start() => 
        HasResource = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            if (resource == Resource)
            {
                Resource.transform.SetParent(transform);

                HasResource = true;
                UnitResourceCollected?.Invoke();
            
                if (Resource.TryGetComponent(out SphereCollider sphereCollider)) 
                    sphereCollider.enabled = false;
            }
        }
    }

    public void SetTargetResource(Resource resource) => 
        Resource = resource;

    public void ClearResource()
    {
        Resource = null;
        HasResource = false;
    }
}