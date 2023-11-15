using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private CollectorSpawner _collectorSpawner;
    [SerializeField] private MineralScanner _mineralScanner;

    private Level _level;

    private void OnValidate()
    {
        _mineralSpawner ??= FindObjectOfType<MineralSpawner>();
        _collectorSpawner ??= FindObjectOfType<CollectorSpawner>();
        _mineralScanner ??= FindObjectOfType<MineralScanner>();
    }

    public void Awake()
    {
        MineralCollection mineralCollection = new MineralCollection();
        CollectorCollection collectorCollection = new CollectorCollection();
        
        _mineralSpawner.Initialize(mineralCollection);
        _collectorSpawner.Initialize(collectorCollection);
        _mineralScanner.Initialize(mineralCollection, collectorCollection);
        
        _level = new Level(_mineralSpawner, mineralCollection, _collectorSpawner, collectorCollection);
        _level.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            _level.Restart();
    }
    
    
}