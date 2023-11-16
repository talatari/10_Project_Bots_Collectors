using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsBusy { get; private set; }
    
    public void Destroy() => Destroy(gameObject);

    public void SetBusy() => IsBusy = true;
    
    
}