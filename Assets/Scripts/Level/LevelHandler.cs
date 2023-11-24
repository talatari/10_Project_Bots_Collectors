using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public bool IsModeSelectSpawnPointStation;
    public bool HaveSpawnPointStation;
    
    private Camera _camera;
    private Vector3 _spawnPoint;
    
    private void Start() => 
        _camera = Camera.main;
    
    public void OnMouseDown()
    {
        if (IsModeSelectSpawnPointStation)
        {
            int normalize = 100;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector3 direction = ray.direction;
        
            _spawnPoint.x = direction.x * normalize;
            _spawnPoint.z = direction.z * normalize;

            IsModeSelectSpawnPointStation = false;
            HaveSpawnPointStation = true;
            
            print($"_spawnPoint = {_spawnPoint}");
        }
    }
    
    public Vector3 GetSpawnPoint() => 
        _spawnPoint;
}