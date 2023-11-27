using UnityEngine;

public class LevelFlager : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    
    private Vector3 _spawnFlagPosition;
    private Flag _currentFlag;
    private Station _station;

    public void SpawFlag(Vector3 spawnFlagPosition)
    {
        if (_currentFlag is not null)
            _currentFlag.Destroy();
        
        Flag newFlag = Instantiate(_flagPrefab, spawnFlagPosition, Quaternion.identity);
        _currentFlag = newFlag;
        _spawnFlagPosition = spawnFlagPosition;

        if (_station is not null)
        {
            _station.BuildStation(_spawnFlagPosition);
        }
    }

    public void SetStation(Station station)
    {
        _station = station;

        if (_currentFlag is not null)
        {
            _station.BuildStation(_spawnFlagPosition);
        }
    }
}