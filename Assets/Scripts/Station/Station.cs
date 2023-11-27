using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] public Material[] Materials;
    [SerializeField] private int _maxCountStation = 10;
    
    private List<Unit> _units = new();
    private Queue<Unit> _freeUnits = new();
    
    private StationUnitSpawner _stationUnitSpawner;
    private StationResourceScanner _stationResourceScanner;
    private StationWallet _stationWallet;
    private UnitBuilder _unitBuilder;
    private LevelRayCaster _levelRayCaster;

    private MeshRenderer _meshRenderer;
    // private int _inActiveMaterial = 0;
    // private int _activeMaterial = 1;
    private Vector3 _buildStationPosition;

    private void Awake()
    {
        _stationUnitSpawner = GetComponent<StationUnitSpawner>();
        _stationUnitSpawner.SetParentStation(this);
        
        _stationResourceScanner = GetComponent<StationResourceScanner>();
        _stationWallet = GetComponent<StationWallet>();
        _levelRayCaster = FindObjectOfType<LevelRayCaster>();
        
        _meshRenderer = GetComponent<MeshRenderer>();
        // _meshRenderer.material = Materials[_inActiveMaterial];
    }

    private void OnEnable()
    {
        _stationWallet.EnoughResourcesForUnit += OnSpawUnit;
        _stationUnitSpawner.SpawnedUnit += OnAddUnit;
        _stationResourceScanner.HaveResourse += OnTryFreeUnitGoWork;
    }

    private void OnDisable()
    {
        _stationWallet.EnoughResourcesForUnit -= OnSpawUnit;
        _stationUnitSpawner.SpawnedUnit -= OnAddUnit;
        _stationResourceScanner.HaveResourse -= OnTryFreeUnitGoWork;
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

    private void OnSpawUnit()
    {
        if (_units.Count < _maxCountStation)
        {
            _stationUnitSpawner.Spaw();
            _stationWallet.DecreaseResourcesForUnit();
        }
    }

    private void OnAddFreeUnitInQueue(Unit unit)
    {
        if (_freeUnits.Contains(unit) == false)
            _freeUnits.Enqueue(unit);
    }

    private void OnTryFreeUnitGoWork()
    {
        TryBuildStation();

        TryCollectResource();
    }

    private void TryBuildStation()
    {
        if (_buildStationPosition != Vector3.zero && _freeUnits.Count > 0)
        {
            Unit freeUnit = _freeUnits.Dequeue();

            if (freeUnit.TryGetComponent(out UnitBuilder unitBuilder))
            {
                unitBuilder.BuildStation(_buildStationPosition);
                _buildStationPosition = new Vector3();
                // TODO: сделать проверку на то хватает ли ресурсов, поднять выше
                _stationWallet.DecreaseResourcesForStation();
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
            Resource resource = _stationResourceScanner.GetResource();

            if (resource is null)
                return;

            Unit freeUnit = _freeUnits.Dequeue();
            freeUnit.CollectResource(resource);
        }
    }
}