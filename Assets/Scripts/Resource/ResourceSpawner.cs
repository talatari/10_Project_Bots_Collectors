using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField, Range(0, 10)] private float _delaySpawn = 1f;
    [SerializeField] private int _minDistance = 5;
    [SerializeField] private int _maxDistance = 48;

    public event Action<Resource> Spawned = delegate { };

    private Station _station;
    private Coroutine _coroutineSpawnWithDelay;
    private Vector3 _stationPosition;
    private float _leftBorder;
    private float _rightBorder;
    private float _downBorder;
    private float _upBorder;

    private void Awake()
    {
        _station = FindObjectOfType<Station>();
        _stationPosition = _station.transform.position;

        _leftBorder = _stationPosition.x - _minDistance;
        _rightBorder = _stationPosition.x + _minDistance;
        _downBorder = _stationPosition.z - _minDistance;
        _upBorder = _stationPosition.z + _minDistance;
    }

    private void OnEnable() => 
        _coroutineSpawnWithDelay = StartCoroutine(SpawnWithDelay());

    private void OnDestroy()
    {
        if (_coroutineSpawnWithDelay is not null)
            StopCoroutine(_coroutineSpawnWithDelay);
    }

    private void SpawnResource()
    {
        Resource resource = Instantiate(_resourcePrefab, GenerateSpawnPosition(), Quaternion.identity);
        resource.transform.parent = gameObject.transform;
        
        Spawned(resource);
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3(
            x: Random.Range(-1 * _maxDistance, _maxDistance),
            y: 0,
            z: Random.Range(-1 * _maxDistance, _maxDistance));

        if (spawnPosition.x >= _leftBorder && spawnPosition.x <= _rightBorder && 
            spawnPosition.z <= _upBorder && spawnPosition.z >= _downBorder)
        {
            if (spawnPosition.x >= _stationPosition.x)
                spawnPosition.x += _minDistance;
            else
                spawnPosition.x -= _minDistance;
            
            if (spawnPosition.z >= _stationPosition.z)
                spawnPosition.z += _minDistance;
            else
                spawnPosition.z -= _minDistance;
        }

        return spawnPosition;
    }

    private IEnumerator<WaitForSeconds> SpawnWithDelay()
    {
        while (true)
        {
            SpawnResource();

            yield return new WaitForSeconds(_delaySpawn);
        }
    }
}