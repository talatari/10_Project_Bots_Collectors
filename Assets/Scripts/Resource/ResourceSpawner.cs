using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField, Range(0, 10)] private float _delaySpawn = 2f;
    [SerializeField] private int _distance = 45;
    
    private ResourceCollection _resourceCollection;
    private Coroutine _coroutineSpawnWithDelay;
    
    public void Initialize(ResourceCollection resourceCollection) => 
        _resourceCollection = resourceCollection;

    public void StartSpawn() => 
        _coroutineSpawnWithDelay = StartCoroutine(SpawnWithDelay());

    private void OnDisable()
    {
        if (_coroutineSpawnWithDelay != null)
            StopCoroutine(_coroutineSpawnWithDelay);
    }

    private void Spawn()
    {
        Vector3 spawnPosition = new Vector3(
            x: Random.Range(_distance * -1, _distance),
            y: 0,
            z: Random.Range(_distance * -1, _distance));

        Resource resource = Instantiate(_resourcePrefab, spawnPosition, Quaternion.identity);
        resource.transform.parent = gameObject.transform;
        _resourceCollection.Add(resource);
    }

    private IEnumerator<WaitForSeconds> SpawnWithDelay()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(_delaySpawn);
        }
    }
    
    
}