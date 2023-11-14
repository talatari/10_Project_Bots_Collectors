public class Level
{
    private MineralSpawner _mineralSpawner;
    private MineralCollection _mineralCollection;
    
    private UnitSpawner _unitSpawner;
    private UnitCollection _unitCollection;

    public Level(MineralSpawner mineralSpawner, MineralCollection mineralCollection, 
                 UnitSpawner unitSpawner, UnitCollection unitCollection)
    {
        _mineralSpawner = mineralSpawner;
        _mineralCollection = mineralCollection;

        _unitSpawner = unitSpawner;
        _unitCollection = unitCollection;
    }

    public void Clear()
    {
        _mineralCollection.Clear();
        _unitCollection.Clear();
    }

    public void Start()
    {
        _mineralSpawner.Spawn();
        _unitSpawner.Spawn();
    }

    public void Restart()
    {
        Clear();
        Start();
    }
    
    
}