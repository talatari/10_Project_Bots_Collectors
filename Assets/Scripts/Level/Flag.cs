using UnityEngine;

public class Flag : MonoBehaviour
{
    public void Destroy() => 
        Destroy(gameObject);
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
            Destroy();
    }
}