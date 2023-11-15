using UnityEngine;

public class MineralScanner : MonoBehaviour
{
    private MineralCollection _mineralCollection;
    private CollectorCollection _collectorCollection;

    public void Initialize(MineralCollection mineralCollection, CollectorCollection collectorCollection)
    {
        _mineralCollection = mineralCollection;
        _collectorCollection = collectorCollection;
    }
    
    private void Scanning()
    {
        while (_mineralCollection.CountFree() > 0 && _collectorCollection.CountFree() > 0)
        {
            Collector collector = _collectorCollection.TryGetFreeUnit();
            Mineral mineral = _mineralCollection.TryGetPositionMineral();
            
            collector.AssignWork(mineral.transform.position);
            mineral.SetBusy();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
            Scanning();
    }
    
    
}