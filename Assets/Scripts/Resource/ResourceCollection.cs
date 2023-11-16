using System.Collections.Generic;

public class ResourceCollection
{
    private Queue<Resource> _resources = new ();

    public void Add(Resource resource) => _resources.Enqueue(resource);

    public Resource TryGetResource()
    {
        if (_resources.Count > 0)
        {
            return _resources.Dequeue();
        }

        return null;
    }

    public void RemoveAll()
    {
        foreach (Resource resource in _resources) 
            resource.Destroy();

        _resources.Clear();
    }
    
    
}