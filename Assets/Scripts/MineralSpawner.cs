using System.Collections.Generic;
using UnityEngine;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Mineral _mineralPrefab;
    [SerializeField] private int _countMinerals = 3;
    
    private MineralCollection _mineralCollection;

    public void Initialize(MineralCollection mineralCollection) => _mineralCollection = mineralCollection;
    
    public void Spawn()
    {
        for (int i = 0; i < _countMinerals; i++)
        {
            Mineral mineral = Instantiate(_mineralPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
            _mineralCollection.Add(mineral);
        }
    }
    
    
}