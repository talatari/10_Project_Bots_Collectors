using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MineralSpawner _mineralSpawner;

    private Level _level;

    public void Awake()
    {
        _mineralSpawner ??= FindObjectOfType<MineralSpawner>();
        
        MineralCollection mineralCollection = new MineralCollection();
        
        _mineralSpawner.Initialize(mineralCollection);
        
        _level = new Level(_mineralSpawner, mineralCollection);
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