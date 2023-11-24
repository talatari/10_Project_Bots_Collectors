using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    
    private Queue<Resource> _resources = new ();

    public bool HaveResource => _resources.Count > 0;
    
    private void OnEnable()
    {
        _resourceSpawner.SpawnResource += OnAddResource;
    }

    private void OnDisable()
    {
        _resourceSpawner.SpawnResource -= OnAddResource;
    }

    public Resource GetResource()
    {
        return _resources.Dequeue();
    }

    private void OnAddResource(Resource resource)
    {
        _resources.Enqueue(resource);
    }
}