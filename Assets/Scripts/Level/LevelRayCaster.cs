using System;
using UnityEngine;

public class LevelRayCaster : MonoBehaviour
{
    private LevelFlager _levelFlager;
    private Camera _camera;

    public event Action<Vector3> HavePoint;
    public event Action<Station> HaveStation; 
    
    public Vector3 Point { get; private set; }

    private void Start() => 
        _camera = Camera.main;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Interact();
    }

    private void Interact()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
        {
            Point = raycastHit.point;

            if (raycastHit.collider.gameObject.TryGetComponent(out Plane plane))
                HavePoint?.Invoke(Point);

            if (raycastHit.collider.gameObject.TryGetComponent(out Station station))
                HaveStation?.Invoke(station);
        }
    }
}