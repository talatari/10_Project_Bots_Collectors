using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speedMove = 25f;

    private Vector3 _targetPoint;
    private bool _haveTarget;

    private void Update()
    {
        // TODO: плохое решение - нагуглить как от этой проверки уйти в сторону события клика мышки
        // TODO: возможно контроль через корутину
        if (_haveTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speedMove * Time.deltaTime);

            if (transform.position == _targetPoint)
                _haveTarget = false;
        }
    }
    
    public void SetTarget(Vector3 targetPoint)
    {
        _targetPoint = targetPoint;
        _haveTarget = true;
    }
}