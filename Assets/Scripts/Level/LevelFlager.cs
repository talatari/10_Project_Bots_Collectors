using System.Collections.Generic;
using UnityEngine;

public class LevelFlager : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private List<Station> _stations = new ();
    private LevelRayCaster _levelRayCaster;
    private Vector3 _spawnFlagPosition;
    private Flag _currentFlag;
    private Station _currentStation;

    private void Awake() => 
        _levelRayCaster = FindObjectOfType<LevelRayCaster>();

    private void OnEnable()
    {
        _levelRayCaster.HavePoint += OnSpawFlag;
        _levelRayCaster.HaveStation += OnSelectStation;
    }

    private void OnDisable()
    {
        _levelRayCaster.HavePoint -= OnSpawFlag;
        _levelRayCaster.HaveStation -= OnSelectStation;
    }

    public void OnSpawFlag(Vector3 spawnFlagPosition)
    {
        if (_currentFlag is not null)
            _currentFlag.Destroy();
        
        Flag newFlag = Instantiate(_flagPrefab, spawnFlagPosition, Quaternion.identity);
        _currentFlag = newFlag;
        _spawnFlagPosition = spawnFlagPosition;

        if (_currentStation is not null)
            _currentStation.BuildStation(_spawnFlagPosition);
    }

    public void OnSelectStation(Station station)
    {
        _currentStation = station;
        
        if (_stations.Contains(station) == false)
            _stations.Add(station);

        foreach (Station _station in _stations)
        {
            if (_station == _currentStation)
                if (_station.IsActive())
                    _station.SetInActive();
                else 
                    _station.SetActive();
            else
                _station.SetInActive();
        }

        if (_currentFlag is not null)
            _currentStation.BuildStation(_spawnFlagPosition);
    }

    public void SetUnitBuilder(Unit unit)
    {
        if (_currentFlag is not null)
        {
            _currentFlag.SetUnitBuilder(unit);
            _currentFlag = null;
        }
        
        _currentStation = null;
    }
}