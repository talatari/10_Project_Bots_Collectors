public class Level
{
    private ResourceSpawner _resourceSpawner;
    private ResourceCollection _resourceCollection;
    private UnitSpawner _unitSpawner;
    private UnitCollection _unitCollection;

    public Level(ResourceSpawner resourceSpawner, ResourceCollection resourceCollection, 
        UnitSpawner unitSpawner, UnitCollection unitCollection)
    {
        _resourceSpawner = resourceSpawner;
        _resourceCollection = resourceCollection;
        _unitSpawner = unitSpawner;
        _unitCollection = unitCollection;
    }

    public void Clear()
    {
        _resourceCollection.RemoveAll();
        _unitCollection.Clear();
    }

    public void Start()
    {
        _resourceSpawner.StartSpawn();
        _unitSpawner.Spawn();
    }

    public void Restart()
    {
        Clear();
        Start();
    }
    
    
}