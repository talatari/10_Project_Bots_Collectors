using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speedMove = 25f;

    private Vector3 _targetPoint;

    private void Update()
    {
        if (transform.position != _targetPoint)
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speedMove * Time.deltaTime);
    }
    
    public void SetTarget(Vector3 targetPoint) => 
        _targetPoint = targetPoint;
}