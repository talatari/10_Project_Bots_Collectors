using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    private Queue<Resource> _resources = new ();
    private ResourceSpawner _resourceSpawner;
    private Coroutine _coroutineHaveResource;
    
    public event Action HaveResourse;

    private void Awake() => 
        _resourceSpawner = GetComponent<ResourceSpawner>();

    private void OnEnable()
    {
        _resourceSpawner.SpawnedResource += OnAddResource;
        
        _coroutineHaveResource = StartCoroutine(HaveResource());
    }

    private void OnDisable()
    {
        _resourceSpawner.SpawnedResource -= OnAddResource;
        
        if (_coroutineHaveResource is not null)
            StopCoroutine(_coroutineHaveResource);
    }

    public Resource GetResource() => 
        _resources.Dequeue();

    private void OnAddResource(Resource resource) => 
        _resources.Enqueue(resource);

    private IEnumerator HaveResource()
    {
        while (true)
        {
            if (_resources.Count > 0)
                HaveResourse?.Invoke();

            yield return null;
        }
    }
}