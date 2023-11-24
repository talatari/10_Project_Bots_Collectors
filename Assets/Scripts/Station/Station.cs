using System;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private StationWallet _stationWallet;
    [SerializeField] private StationCollector _stationCollector;
    [SerializeField] private ResourceScanner _resourceScanner;
    
    private List<Unit> _units;
    private bool _isModeSelectParentStation;
    private Vector3 _stationSpawnPosition;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.UnitCollector.HasResource == false)
            {
                if (_resourceScanner.HaveResource && unit.IsWork == false)
                {
                    unit.CollectResource(_resourceScanner.GetResource());
                }
                
                return;
            }

            _stationCollector.Collect(unit);
        }
    }
    
    public void OnMouseDown()
    {
        if (_isModeSelectParentStation)
        {
            _isModeSelectParentStation = false;
        }
    }

    public void CreateStation() => 
        _isModeSelectParentStation = true;
}