using UnityEngine;

public class LevelFlager : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private LevelRayCaster _levelRayCaster;
    private Vector3 _spawnFlagPosition;
    private Flag _currentFlag;
    private Station _station;
    
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

        if (_station is not null)
            _station.BuildStation(_spawnFlagPosition);
    }

    public void OnSelectStation(Station station)
    {
        _station = station;

        if (_currentFlag is not null)
            _station.BuildStation(_spawnFlagPosition);
    }

    public void SetUnitBuilder(Unit unit)
    {
        _currentFlag.SetUnitBuilder(unit);
    }
}