using System;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    
    private List<Unit> _units = new ();
    private StationUnitSpawner _stationUnitSpawner;
    private ResourceScanner _resourceScanner;
    private StationWallet _stationWallet;
    private LevelRayCaster _levelRayCaster;
    private UnitBuilder _unitBuilder;
    private MeshRenderer _meshRenderer;
    private UISpawnUnitButtonHandler _uiSpawnUnitButtonHandler;
    private UISpawnStationButtonHandler _uiSpawnStationButtonHandler;
    private bool _isModeBuildStation;
    private int _inActiveMaterial = 0;
    private int _activeMaterial = 1;

    private void Awake()
    {
        _stationUnitSpawner = GetComponent<StationUnitSpawner>();
        _stationUnitSpawner.SetParentStation(this);
        _resourceScanner = FindObjectOfType<ResourceScanner>();
        _stationWallet = FindObjectOfType<StationWallet>();
        _levelRayCaster = FindObjectOfType<LevelRayCaster>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _materials[_inActiveMaterial];
    }

    private void Start()
    {
        _uiSpawnUnitButtonHandler = FindObjectOfType<UISpawnUnitButtonHandler>();
        _uiSpawnStationButtonHandler = FindObjectOfType<UISpawnStationButtonHandler>();
    }

    private void OnEnable()
    {
        _stationUnitSpawner.SpawnedUnit += OnAddUnit;
        _resourceScanner.HaveResourse += OnFreeUnitGoWork;
    }

    private void OnMouseDown()
    {
        if (_isModeBuildStation)
        {
            _meshRenderer.material = _materials[_activeMaterial];
            _uiSpawnUnitButtonHandler.SetStation(this);
            _uiSpawnStationButtonHandler.SetStation(this);
            _isModeBuildStation = false;
        }
        else
        {
            _meshRenderer.material = _materials[_inActiveMaterial];
            _isModeBuildStation = true;
        }
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

    public void SpawnStationHadle() => 
        _isModeBuildStation = true;

    public void OnAddUnit(Unit unit) => 
        _units.Add(unit);

    public void RemoveUnit(Unit unit) => 
        _units.Remove(unit);

    private Unit TryGetFreeUnit()
    {
        foreach (Unit unit in _units)
            if (unit.IsWork == false)
                return unit;

        return null;
    }

    private void OnFreeUnitGoWork()
    {
        Unit freeUnit = TryGetFreeUnit();
        
        if (_isModeBuildStation && _levelRayCaster.HaveSpawnPoint)
        {
            if (freeUnit is not null)
            {
                freeUnit.SpawnStation(_levelRayCaster.GetSpawnPoint());
                _stationWallet.DecreaseResourcesForStation();
            }
        }
        else
        {
            if (freeUnit is not null)
            {
                Resource resource = _resourceScanner.GetResource();
                
                if (resource is not null)
                    freeUnit.CollectResource(resource);
            }
        }
    }
}