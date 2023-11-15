using UnityEngine;

public class MineralScanner : MonoBehaviour
{
    private MineralCollection _mineralCollection;
    private UnitCollection _unitCollection;

    public void Initialize(MineralCollection mineralCollection, UnitCollection unitCollection)
    {
        _mineralCollection = mineralCollection;
        _unitCollection = unitCollection;
    }
    
    private void Scanning()
    {
        while (_mineralCollection.Count > 0)
        {
            Unit unit = _unitCollection.TryGetFreeUnit();
            Mineral mineral = _mineralCollection.TryGetPositionMineral();
            
            unit?.AssignWork(mineral.transform.position);
            mineral?.SetBusy();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Scanning();
        }
    }
}