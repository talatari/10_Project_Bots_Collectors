using UnityEngine;

public class LevelRayCaster : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _point;
    private string _name;
    private Vector3 _position;

    public Vector3 Point => _point;
    public string Name => _name;
    public Vector3 Position => _position;

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
            _point = raycastHit.point;
            _name = raycastHit.collider.gameObject.name;
            _point = raycastHit.transform.position;

            // print($"raycastHit.point = {raycastHit.point}");
            // print($"raycastHit.collider.gameObject.name = {raycastHit.collider.gameObject.name}");
            // print($"raycastHit.transform.position = {raycastHit.transform.position}");
        }
    }
}