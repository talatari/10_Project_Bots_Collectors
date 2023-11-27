using System;
using System.Collections.Generic;
using UnityEngine;

public class StationResourceScanner : MonoBehaviour
{
    private Queue<Resource> _resources = new ();
    private ResourceSpawner _resourceSpawner;
    
    public event Action HaveResourse;

    private void Awake() => 
        _resourceSpawner = FindObjectOfType<ResourceSpawner>();

    private void OnEnable() =>
        _resourceSpawner.SpawnedResource += OnAddResource;

    private void OnDisable() => 
        _resourceSpawner.SpawnedResource -= OnAddResource;

    public Resource GetResource()
    {
        if (_resources.Count > 0)
            return _resources.Dequeue();

        return null;
    }
    
    private void OnAddResource(Resource resource)
    {
        _resources.Enqueue(resource);
        HaveResourse?.Invoke();
    }
}