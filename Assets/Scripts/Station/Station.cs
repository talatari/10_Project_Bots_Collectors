using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private List<Unit> _units = new ();
    private StationUnitSpawner _stationUnitSpawner;
    private StationWallet _stationWallet;
    private ResourceScanner _resourceScanner;
    private LevelRayCaster _levelRayCaster;
    private bool _isModeBuildStation;

    private void Awake()
    {
        _stationUnitSpawner = GetComponent<StationUnitSpawner>();
        _stationUnitSpawner.SetStationPosition(transform.position);
        _stationWallet = GetComponent<StationWallet>();
        _resourceScanner = FindObjectOfType<ResourceScanner>();
        _levelRayCaster = FindObjectOfType<LevelRayCaster>();
    }

    private void OnEnable()
    {
        _stationUnitSpawner.SpawnedUnit += OnAddUnit;
        _resourceScanner.HaveResourse += OnFreeUnitGoWork;
    }

    public void OnMouseDown()
    {
        if (_isModeBuildStation)
            _levelRayCaster.SetStation(this);
    }
    
    private void OnDisable()
    {
        _stationUnitSpawner.SpawnedUnit -= OnAddUnit;
        _resourceScanner.HaveResourse -= OnFreeUnitGoWork;
    }

    public void SpawnUnitHadle()
    {
        _stationUnitSpawner.Spaw();
        _stationWallet.DecreaseResourcesForUnit();
    }

    public void SpawnStationHadle()
    {
        _isModeBuildStation = true;
    }

    private Unit TryGetFreeUnit()
    {
        foreach (Unit unit in _units)
            if (unit.IsWork == false)
                return unit;

        return null;
    }

    private void OnAddUnit(Unit unit) => 
        _units.Add(unit);

    private void OnFreeUnitGoWork()
    {
        Unit freeUnit = TryGetFreeUnit();
        
        if (_isModeBuildStation)
        {
            if (freeUnit is not null)
            {
                freeUnit.SpawnStation(_levelRayCaster.GetSpawnPoint());
                _isModeBuildStation = false;
                _stationWallet.DecreaseResourcesForStation();
            }
        }
        else
        {
            if (freeUnit is not null)
                freeUnit.CollectResource(_resourceScanner.GetResource());
        }
    }
}