using UnityEngine;

public class StationSpawner : MonoBehaviour
{
    [SerializeField] private Station _stationPrefab;
    [SerializeField] private LevelHandler _levelHandler;
    
    public void SpawStation() => 
        Instantiate(_stationPrefab, _levelHandler.GetSpawnPoint(), Quaternion.identity);
}