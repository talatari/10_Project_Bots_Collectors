using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private int _maxCountStation = 10;
    
    private List<Unit> _units = new();
    private Queue<Unit> _freeUnits = new();
    
    private StationUnitSpawner _stationUnitSpawner;
    private StationResourceScanner _stationResourceScanner;
    private StationWallet _stationWallet;
    private LevelRayCaster _levelRayCaster;
    private UnitBuilder _unitBuilder;
    
    private MeshRenderer _meshRenderer;
    private int _inActiveMaterial = 0;
    // private int _activeMaterial = 1;

    private void Awake()
    {
        _stationUnitSpawner = GetComponent<StationUnitSpawner>();
        _stationUnitSpawner.SetParentStation(this);
        
        _stationResourceScanner = GetComponent<StationResourceScanner>();
        _stationWallet = GetComponent<StationWallet>();
        _levelRayCaster = FindObjectOfType<LevelRayCaster>();
        
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _materials[_inActiveMaterial];
    }

    private void OnEnable()
    {
        _stationWallet.EnoughResourcesForUnit += OnSpawUnit;
        _stationUnitSpawner.SpawnedUnit += OnAddUnit;
        _stationResourceScanner.HaveResourse += OnTryCollectResource;
    }

    private void OnDisable()
    {
        _stationWallet.EnoughResourcesForUnit -= OnSpawUnit;
        _stationUnitSpawner.SpawnedUnit -= OnAddUnit;
        _stationResourceScanner.HaveResourse -= OnTryCollectResource;
    }

    private void OnSpawUnit()
    {
        if (_units.Count < _maxCountStation)
        {
            _stationUnitSpawner.Spaw();
            _stationWallet.DecreaseResourcesForUnit();
        }
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
    
    private void OnAddFreeUnitInQueue(Unit unit)
    {
        if (_freeUnits.Contains(unit) == false)
            _freeUnits.Enqueue(unit);
    }

    private void OnTryCollectResource()
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