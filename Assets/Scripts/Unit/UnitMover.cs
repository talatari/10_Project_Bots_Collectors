using UnityEngine;

[RequireComponent(
    typeof(Unit))]

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speedMove = 25f;

    private Unit _unit;
    private Transform _targetPoint;

    private void Start() => 
        _unit = GetComponent<Unit>();

    private void Update()
    {
        if (_targetPoint is not null)
            transform.position = Vector3.MoveTowards(
                transform.position, _targetPoint.position, _speedMove * Time.deltaTime);
    }
    
    public void SetTarget(Transform targetPoint) => _targetPoint = targetPoint;

    
}