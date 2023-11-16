using System.Collections.Generic;

public class ResourceCollection
{
    private List<Resource> _resources = new ();

    public int CountFree()
    {
        int countFree = 0;

        foreach (Resource resource in _resources)
            if (resource.IsBusy == false)
                countFree++;

        return countFree;
    }

    public void Add(Resource resource) => _resources.Add(resource);

    public Resource TryGetPositionResource()
    {
        foreach (Resource resource in _resources)
            if (resource.IsBusy == false)
                return resource;

        return null;
    }

    public void RemoveAll()
    {
        foreach (Resource resource in _resources) 
            resource.Destroy();

        _resources.Clear();
    }

    public void Remove(Resource resource)
    {
        _resources.Remove(resource);
        
        resource.Destroy();
    }
    
    
}