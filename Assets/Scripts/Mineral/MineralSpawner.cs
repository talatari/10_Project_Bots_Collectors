using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Mineral _mineralPrefab;
    [SerializeField, Range(0, 10)] private float _delaySpawn = 2f;
    [SerializeField] private int _distance = 45;
    [SerializeField] private int _count;
    
    private MineralCollection _mineralCollection;
    private Coroutine _coroutineSpawnWithDelay;
    
    public void Initialize(MineralCollection mineralCollection) => _mineralCollection = mineralCollection;

    public void StartSpawn() => _coroutineSpawnWithDelay = StartCoroutine(SpawnWithDelay());

    private void OnDisable()
    {
        if (_coroutineSpawnWithDelay != null)
        {
            StopCoroutine(_coroutineSpawnWithDelay);
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = new Vector3(
            x: Random.Range(_distance * -1, _distance),
            y: 0,
            z: Random.Range(_distance * -1, _distance));

        Mineral mineral = Instantiate(_mineralPrefab, spawnPosition, Quaternion.identity);

        mineral.transform.parent = gameObject.transform;
        
        _mineralCollection.Add(mineral);
        _count++;
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