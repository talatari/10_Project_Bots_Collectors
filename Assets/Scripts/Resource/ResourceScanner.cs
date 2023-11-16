using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    private ResourceCollection _resourceCollection;
    private UnitCollection _unitCollection;
    private Station _station;

    private void Start() => 
        _station.UnitCollectorFree += Scanning;

    private void OnDestroy() => 
        _station.UnitCollectorFree -= Scanning;
    
    public void Initialize(ResourceCollection resourceCollection, UnitCollection unitCollection, Station station)
    {
        _resourceCollection = resourceCollection;
        _unitCollection = unitCollection;
        _station = station;
    }

    private void Scanning()
    {
        Resource resource = _resourceCollection.TryGetResource();
        Unit unit = _unitCollection.TryGetFreeUnit();

        if (resource is not null && unit is not null)
            unit.AssignWork(resource);
        else
            _resourceCollection.Add(resource);
    }
    
    
}