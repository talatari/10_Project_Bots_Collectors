using UnityEngine;

public class LevelRayCaster : MonoBehaviour
{
    private LevelFlager _levelFlager;
    private Camera _camera;
    
    public Vector3 Point { get; private set; }
    public string Name { get; private set; }
    public Vector3 Position { get; private set; }

    private void Awake() => 
        _levelFlager = FindObjectOfType<LevelFlager>();

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
            Name = raycastHit.collider.gameObject.name;
            Position = raycastHit.transform.position;

            if (raycastHit.collider.gameObject.TryGetComponent(out Plane plane))
                _levelFlager.SpawFlag(Point);

            if (raycastHit.collider.gameObject.TryGetComponent(out Station station))
                _levelFlager.SetStation(station);
        }
    }
}