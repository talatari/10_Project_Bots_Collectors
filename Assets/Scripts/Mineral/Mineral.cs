using UnityEngine;

public class Mineral : MonoBehaviour
{
    public bool IsBusy { get; private set; }
    
    public void Clear() => Destroy(gameObject);

    public void SetBusy() => IsBusy = true;
    
    
}