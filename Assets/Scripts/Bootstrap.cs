using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Station _station;
    
    private Level _level;

    public void Awake()
    {
        ResourceCollection resourceCollection = new ResourceCollection();
        UnitCollection unitCollection = new UnitCollection();
        
        _resourceSpawner.Initialize(resourceCollection);
        _unitSpawner.Initialize(unitCollection);
        
        _resourceScanner.Initialize(resourceCollection, unitCollection, _station);
        _station.Initialize(resourceCollection);
        
        _level = new Level(_resourceSpawner, resourceCollection, _unitSpawner, unitCollection);
        _level.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            _level.Restart();
    }
    
    
}