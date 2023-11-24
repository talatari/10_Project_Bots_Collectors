using UnityEngine;

public class LevelRayCaster : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _spawnPoint;
    private Station _station;
    
    
    private void Start() => 
        _camera = Camera.main;
    
    public void OnMouseDown()
    {
        int normalize = 100;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = ray.direction;
    
        _spawnPoint.x = direction.x * normalize;
        _spawnPoint.z = direction.z * normalize;
        
        print("Запомнили точку спавна новой базы в LevelRayCaster");
        print(_spawnPoint);
    }

    public void SetStation(Station station)
    {
        _station = station;
        print("Передали родительскую базу в LevelRayCaster");
        print(_station.transform.position);
    }
    
    public Vector3 GetSpawnPoint() => 
        _spawnPoint;
}