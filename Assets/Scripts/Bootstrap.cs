using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private MineralScanner _mineralScanner;

    private Level _level;

    public void Awake()
    {
        _mineralSpawner ??= FindObjectOfType<MineralSpawner>();
        _unitSpawner ??= FindObjectOfType<UnitSpawner>();
        _mineralScanner ??= FindObjectOfType<MineralScanner>();
        
        MineralCollection mineralCollection = new MineralCollection();
        UnitCollection unitCollection = new UnitCollection();
        
        _mineralSpawner.Initialize(mineralCollection);
        _unitSpawner.Initialize(unitCollection);
        _mineralScanner.Initialize(mineralCollection, unitCollection);
        
        _level = new Level(_mineralSpawner, mineralCollection, _unitSpawner, unitCollection);
        _level.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _level.Restart();
        }
    }
    
    
}