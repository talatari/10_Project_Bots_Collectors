using UnityEngine;

public class LevelRayCaster : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _spawnPoint;
    
    public bool HaveSpawnPoint { get; private set; }
    
    private void Start() => 
        _camera = Camera.main;
    
    public void OnMouseDown()
    {
        int normalize = 100;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = ray.direction;
    
        _spawnPoint.x = direction.x * normalize;
        _spawnPoint.z = direction.z * normalize;

        HaveSpawnPoint = true;
    }
    
    public Vector3 GetSpawnPoint()
    {
        HaveSpawnPoint = false;
        return _spawnPoint;
    }
}