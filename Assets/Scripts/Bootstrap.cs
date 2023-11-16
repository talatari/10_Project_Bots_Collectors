using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private Base _base;
    
    private Level _level;

    private void Start()
    {
        _resourceSpawner ??= FindObjectOfType<ResourceSpawner>();
        _unitSpawner ??= FindObjectOfType<UnitSpawner>();
        _resourceScanner ??= FindObjectOfType<ResourceScanner>();
        _base ??= FindObjectOfType<Base>();
    }

    public void Awake()
    {
        ResourceCollection resourceCollection = new ResourceCollection();
        UnitCollection unitCollection = new UnitCollection();
        
        _resourceSpawner.Initialize(resourceCollection);
        _unitSpawner.Initialize(unitCollection);
        
        _resourceScanner.Initialize(resourceCollection, unitCollection);
        _base.Initialize(resourceCollection);
        
        _level = new Level(_resourceSpawner, resourceCollection, _unitSpawner, unitCollection);
        _level.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            _level.Restart();
    }
    
    
}