public class Level
{
    private MineralSpawner _mineralSpawner;
    private MineralCollection _mineralCollection;

    public Level(MineralSpawner mineralSpawner, MineralCollection mineralCollection)
    {
        _mineralSpawner = mineralSpawner;
        _mineralCollection = mineralCollection;
    }

    public void Clear() => _mineralCollection.Clear();
    
    public void Start() => _mineralSpawner.Spawn();

    public void Restart()
    {
        Clear();
        Start();
    }
    
    
}