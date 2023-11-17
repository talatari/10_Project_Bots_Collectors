using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField, Range(0, 10)] private float _delaySpawn = 1f;
    [SerializeField] private int _minDistance = 5;
    [SerializeField] private int _maxDistance = 45;
    
    private ResourceCollection _resourceCollection;
    private Coroutine _coroutineSpawnWithDelay;
    private Vector3[] _matrixSpawnPosition;

    private void Awake() => 
        InitializeMatrixSpawnPosition();

    private void OnDisable()
    {
        if (_coroutineSpawnWithDelay is not null)
            StopCoroutine(_coroutineSpawnWithDelay);
    }
    
    public void Initialize(ResourceCollection resourceCollection) => 
        _resourceCollection = resourceCollection;
    
    private void InitializeMatrixSpawnPosition()
    {
        // TODO: автоматизировать! если делать элементов с запасом, то будут нулевые значения вектором.
        // TODO: 121 - это при _minDistance = 5. 961 - при _minDistance = 15.
        _matrixSpawnPosition = new Vector3[121];
        int index = 0;
        
        for (int x = -1 *_minDistance; x <= _minDistance; x++)
            for (int z = -1 * _minDistance; z <= _minDistance; z++)
                _matrixSpawnPosition[index++] = new Vector3(x, 0, z);
    }

    public void StartSpawn() => 
        _coroutineSpawnWithDelay = StartCoroutine(SpawnWithDelay());

    private void Spawn()
    {
        Resource resource = Instantiate(_resourcePrefab, GenerateSpawnPosition(), Quaternion.identity);
        resource.transform.parent = gameObject.transform;
        _resourceCollection.Add(resource);
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(
            x: Random.Range(-1 * _maxDistance, _maxDistance),
            y: 0,
            z: Random.Range(-1 * _maxDistance, _maxDistance));

        foreach (Vector3 noSpawnPosition in _matrixSpawnPosition)
            if (spawnPosition == noSpawnPosition)
            {
                spawnPosition.x += _minDistance;
                spawnPosition.z += _minDistance;
            }

        return spawnPosition;
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