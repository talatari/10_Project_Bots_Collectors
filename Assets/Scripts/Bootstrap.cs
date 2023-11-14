using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MineralSpawner _mineralSpawner;
    [SerializeField] private UnitSpawner _unitSpawner;

    private Level _level;

    public void Awake()
    {
        _mineralSpawner ??= FindObjectOfType<MineralSpawner>();
        _unitSpawner ??= FindObjectOfType<UnitSpawner>();
        
        MineralCollection mineralCollection = new MineralCollection();
        UnitCollection unitCollection = new UnitCollection();
        
        _mineralSpawner.Initialize(mineralCollection);
        _unitSpawner.Initialize(unitCollection);
        
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