using UnityEngine;

public class Flag : MonoBehaviour
{
    private Unit _unit;
    
    public void Destroy() => 
        Destroy(gameObject);

    public void SetUnitBuilder(Unit unit) => 
        _unit = unit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            if (unit == _unit)
                Destroy();
    }
}