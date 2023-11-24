using System;
using UnityEngine;

public class StationCollector : MonoBehaviour
{
    public event Action CollectResource; 

    public void Collect(Unit unit)
    {
        unit.UnitCollector.Resource.Destroy();
        unit.SetFree();
        
        CollectResource?.Invoke();
    }
}