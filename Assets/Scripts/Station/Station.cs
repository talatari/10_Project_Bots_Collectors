using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private int _maxCountStation = 10;
    [SerializeField] private Material[] _materials;
    
    private List<Unit> _units = new();
    private Queue<Unit> _freeUnits = new();
    private StationUnitSpawner _stationUnitSpawner;
    private StationResourceScanner _stationResourceScanner;
    private StationWallet _stationWallet;
    private UnitBuilder _unitBuilder;
    private LevelFlager _levelFlager;
    private MeshRenderer _meshRenderer;
    private Vector3 _buildStationPosition;
    private bool _haveWorkBuildStation;
    private int _inActive = 0;
    private int _active = 1;

    private void Awake()
    {
        _stationUnitSpawner = GetComponent<StationUnitSpawner>();
        _stationUnitSpawner.SetParentStation(this);
        
        _stationResourceScanner = FindObjectOfType<StationResourceScanner>();
        _stationWallet = GetComponent<StationWallet>();
        _levelFlager = FindObjectOfType<LevelFlager>();
        
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _materials[_inActive];
    }

    private void OnEnable()
    {
        _stationWallet.EnoughResourcesForUnit += OnSpawUnit;
        _stationWallet.EnoughResourcesForStation += TryBuildStation;
        _stationUnitSpawner.SpawnedUnit += OnAddUnit;
        _stationResourceScanner.HaveResourse += TryCollectResource;
    }

    private void OnDisable()
    {
        _stationWallet.EnoughResourcesForUnit -= OnSpawUnit;
        _stationWallet.EnoughResourcesForStation -= TryBuildStation;
        _stationUnitSpawner.SpawnedUnit -= OnAddUnit;
        _stationResourceScanner.HaveResourse -= TryCollectResource;
    }

    public void OnAddUnit(Unit unit)
    {
        unit.UnitFree += OnAddFreeUnitInQueue;
        _units.Add(unit);

        if (_freeUnits.Contains(unit) == false)
            _freeUnits.Enqueue(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unit.UnitFree -= OnAddFreeUnitInQueue;
        _units.Remove(unit);
    }

    public void BuildStation(Vector3 buildStationPosition) => 
        _buildStationPosition = buildStationPosition;

    public void SetInActive()
    {
        _meshRenderer.material = _materials[_inActive];
        _haveWorkBuildStation = false;
    }

    public void SetActive()
    {
        _meshRenderer.material = _materials[_active];
        _haveWorkBuildStation = true;
    }

    public bool IsActive()
    {
        string postfix = " (Instance)";
        return _meshRenderer.material.name == _materials[_active].name + postfix;
    }

    private void OnSpawUnit()
    {
        int mixUnitsStation = 2;
        
        if (_units.Count < _maxCountStation && _haveWorkBuildStation == false || _units.Count < mixUnitsStation)
        {
            _stationUnitSpawner.Spaw();
            _stationWallet.DecreaseResourcesForUnit();
        }
    }

    private void OnAddFreeUnitInQueue(Unit unit)
    {
        if (_freeUnits.Contains(unit) == false)
            _freeUnits.Enqueue(unit);
        
        _stationWallet.CanSpawnUnit();
    }

    private void TryBuildStation()
    {
        if (_haveWorkBuildStation && _freeUnits.Count > 0)
        {
            Unit freeUnit = _freeUnits.Dequeue();

            if (freeUnit.TryGetComponent(out UnitBuilder unitBuilder))
            {
                unitBuilder.BuildStation(_buildStationPosition);
                _levelFlager.SetUnitBuilder(freeUnit);
                _stationWallet.DecreaseResourcesForStation();
                SetInActive();
                _haveWorkBuildStation = false;
            }
            else
            {
                _freeUnits.Enqueue(freeUnit);
            }
        }
    }

    private void TryCollectResource()
    {
        while (_freeUnits.Count != 0)
        {
            if (_stationResourceScanner.TryGetResource(out Resource resource))
            {
                Unit freeUnit = _freeUnits.Dequeue();
                freeUnit.CollectResource(resource);
            }
            else
            { 
                break;
            }
        }
    }
}