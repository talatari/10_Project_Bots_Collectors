using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    
    private List<Unit> _units = new();
    private Queue<Unit> _freeUnits = new();
    private StationUnitSpawner _stationUnitSpawner;
    private ResourceScanner _resourceScanner;
    private StationWallet _stationWallet;
    private LevelRayCaster _levelRayCaster;
    private UnitBuilder _unitBuilder;
    private MeshRenderer _meshRenderer;
    private bool _isModeBuildStation;
    private int _inActiveMaterial = 0;
    private int _activeMaterial = 1;

    private void Awake()
    {
        _stationUnitSpawner = GetComponent<StationUnitSpawner>();
        _stationUnitSpawner.SetParentStation(this);
        _resourceScanner = FindObjectOfType<ResourceScanner>();
        _stationWallet = GetComponent<StationWallet>();
        _levelRayCaster = FindObjectOfType<LevelRayCaster>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _materials[_inActiveMaterial];
    }

    private void OnEnable()
    {
        _stationUnitSpawner.SpawnedUnit += AddUnit;
        _resourceScanner.HaveResourse += OnFreeUnitGoWork;
    }

    private void OnMouseDown()
    {
        if (_isModeBuildStation)
        {
            _meshRenderer.material = _materials[_activeMaterial];
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
        _stationUnitSpawner.SpawnedUnit -= AddUnit;
        _resourceScanner.HaveResourse -= OnFreeUnitGoWork;
    }

    public void SpawnUnitHadle()
    {
        _stationUnitSpawner.Spaw();
        _stationWallet.DecreaseResourcesForUnit();
    }

    public void SpawnStationHadle() => 
        _isModeBuildStation = true;

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
        _freeUnits.Enqueue(unit);
        unit.UnitFree += OnAddFreeUnitInQueue;
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
        _freeUnits.Dequeue(unit);
        unit.UnitFree -= OnAddFreeUnitInQueue;
    }

    private void OnAddFreeUnitInQueue()
    {
        
    }
    
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
        
        if (freeUnit is null)
            return;
        
        if (_isModeBuildStation && _levelRayCaster.HaveSpawnPoint)
        {
            freeUnit.SpawnStation(_levelRayCaster.GetSpawnPoint());
            _stationWallet.DecreaseResourcesForStation();
            return;
        }

        Resource resource = _resourceScanner.GetResource();
        
        if (resource is not null)
            freeUnit.CollectResource(resource);
    }
}