using System;
using System.Collections.Generic;
using UnityEngine;

public class StationResourceScanner : MonoBehaviour
{
    private Queue<Resource> _resources = new ();
    private ResourceSpawner _resourceSpawner;
    
    public event Action HaveResourse;

    private void Awake() => 
        _resourceSpawner = GetComponent<ResourceSpawner>();

    private void OnEnable() =>
        _resourceSpawner.SpawnedResource += OnAddResource;

    private void OnDisable() => 
        _resourceSpawner.SpawnedResource -= OnAddResource;

    public bool TryGetResource(out Resource resource)
    {
        if (_resources.Count > 0)
        {
            resource = _resources.Dequeue();
            return true;
        }

        resource = null;
        return false;
    }
    
    private void OnAddResource(Resource resource)
    {
        _resources.Enqueue(resource);
        HaveResourse?.Invoke();
    }
}