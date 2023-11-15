using UnityEngine;

public class CollectorMovement : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private float _speedMove = 5f;
    
    // private Mineral _targetMineral;
    private Transform _targetPoint;

    private void OnValidate() => _collector ??= GetComponent<Collector>();

    public void Update()
    {
        if (_targetPoint is not null)
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _speedMove * Time.deltaTime);

        // if (_targetPoint.position == Vector3.zero && _collector.IsWork && _targetMineral is not null)
        //     _targetMineral.transform.position = transform.position;

        // if (transform.position == _targetPoint.position)
        //     _targetPoint.position = Vector3.zero;
    }
    
    public void SetTarget(Transform targetPoint) => _targetPoint = targetPoint;

    public void ClearTarget()
    {
        // _targetMineral = null;
        _collector.SetFree();
    }
    
    
}