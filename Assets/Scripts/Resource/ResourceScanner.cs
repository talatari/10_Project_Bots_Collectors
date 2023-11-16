using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    private ResourceCollection _resourceCollection;
    private UnitCollection _unitCollection;

    public void Initialize(ResourceCollection resourceCollection, UnitCollection unitCollection)
    {
        _resourceCollection = resourceCollection;
        _unitCollection = unitCollection;
    }
    
    private void Scanning()
    {
        while (_resourceCollection.CountFree() > 0 && _unitCollection.CountFree() > 0)
        {
            Unit unit = _unitCollection.TryGetFreeUnit();
            Resource resource = _resourceCollection.TryGetPositionResource();
            
            unit.AssignWork(resource);
            resource.SetBusy();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
            Scanning();
    }
    
    
}