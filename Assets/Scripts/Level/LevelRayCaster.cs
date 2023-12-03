using System;
using UnityEngine;

public class LevelRayCaster : MonoBehaviour
{
    private LevelFlager _levelFlager;
    private Camera _camera;

    public event Action<Vector3> HavePoint = delegate {  };
    public event Action<Station> HaveStation = delegate { }; 
    
    public Vector3 Point { get; private set; }

    private void Start() => 
        _camera = Camera.main;
    
    private void Update()
    {
        // TODO: плохое решение - нагуглить как от этой проверки уйти в сторону события клика мышки
        // TODO: возможно контроль через корутину
        if (Input.GetMouseButtonDown(0))
            Interact();
    }

    private void Interact()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
        {
            Point = raycastHit.point;

            if (raycastHit.collider.gameObject.TryGetComponent(out Plane plane))
                HavePoint(Point);

            if (raycastHit.collider.gameObject.TryGetComponent(out Station station))
                HaveStation(station);
        }
    }
}