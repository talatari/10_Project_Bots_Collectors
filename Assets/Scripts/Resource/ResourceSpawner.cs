using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField, Range(0, 10)] private float _delaySpawn = 1f;
    [SerializeField] private int _maxDistance = 45;
    
    private ResourceCollection _resourceCollection;
    private Coroutine _coroutineSpawnWithDelay;
    
    private void OnDisable()
    {
        if (_coroutineSpawnWithDelay is not null)
            StopCoroutine(_coroutineSpawnWithDelay);
    }
    
    public void Initialize(ResourceCollection resourceCollection) => 
        _resourceCollection = resourceCollection;

    public void StartSpawn() => 
        _coroutineSpawnWithDelay = StartCoroutine(SpawnWithDelay());

    private void Spawn()
    {
        Resource resource = Instantiate(_resourcePrefab, GenerateSpawnPosition(), Quaternion.identity);
        resource.transform.parent = gameObject.transform;
        _resourceCollection.Add(resource);
    }

    private Vector3 GenerateSpawnPosition() => 
        new(x: SpawnPositionValidate(), y: 0, z: SpawnPositionValidate());

    private float SpawnPositionValidate()
    {
        float border = 5;
        float offset = 15;
        float randomPosition = Random.Range(-1 * _maxDistance, _maxDistance);

        if (randomPosition > -1 * border && randomPosition < border)
            randomPosition += offset;

        return randomPosition;
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