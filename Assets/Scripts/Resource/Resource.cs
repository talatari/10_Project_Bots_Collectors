using UnityEngine;

public class Resource : MonoBehaviour
{
    public int Units;
        
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}