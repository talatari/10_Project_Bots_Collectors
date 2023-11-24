using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    private ResourceCollection _resourceCollection;
    private UnitCollection _unitCollection;
    private Station _station;

    private void Start()
    {
        if (_station is not null)
            _station.UnitCollectorFree += Scanning;
    }

    private void OnDestroy()
    {
        if (_station is not null)
            _station.UnitCollectorFree -= Scanning;
    }

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
        {
            print("unit.CollectResource(resource);");
            unit.CollectResource(resource);
        }
        else
        {
            print("_resourceCollection.Add(resource);");
            _resourceCollection.Add(resource);
        }
            
    }
}