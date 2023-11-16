using System;
using UnityEngine;

public class UnitCollector: MonoBehaviour
{
    public event Action ResourceCollected;
    
    public bool HasResource { get; private set; }
    public Resource Resource { get; private set; }
    
    private void Start() => HasResource = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Resource resource))
        {
            if (resource == Resource)
            {
                Resource.transform.SetParent(transform);

                HasResource = true;
                ResourceCollected?.Invoke();
            
                if (Resource.TryGetComponent(out SphereCollider sphereCollider)) 
                    sphereCollider.enabled = false;
            }
        }
    }

    public void SetTargetResource(Resource resource) => Resource = resource;

    public void ClearResource()
    {
        Resource = null;
        HasResource = false;
    }
    
    
}