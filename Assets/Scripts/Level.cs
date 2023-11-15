public class Level
{
    private MineralSpawner _mineralSpawner;
    private MineralCollection _mineralCollection;
    
    private CollectorSpawner _collectorSpawner;
    private CollectorCollection _collectorCollection;

    public Level(MineralSpawner mineralSpawner, MineralCollection mineralCollection,
        CollectorSpawner collectorSpawner, CollectorCollection collectorCollection)
    {
        _mineralSpawner = mineralSpawner;
        _mineralCollection = mineralCollection;

        _collectorSpawner = collectorSpawner;
        _collectorCollection = collectorCollection;
    }

    public void Clear()
    {
        _mineralCollection.Clear();
        _collectorCollection.Clear();
    }

    public void Start()
    {
        _mineralSpawner.StartSpawn();
        _collectorSpawner.Spawn();
    }

    public void Restart()
    {
        Clear();
        Start();
    }
    
    
}