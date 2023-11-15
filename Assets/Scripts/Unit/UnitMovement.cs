using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _speedMove = 5f;
    
    private Vector3 _target;

    public void Update()
    {
        if (_target != null && _target != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speedMove * Time.deltaTime);
        }
    }

    public void Set(Vector3 target) => _target = target;
    
    
}