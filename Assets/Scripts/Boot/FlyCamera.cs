using UnityEngine;
 
public class FlyCamera : MonoBehaviour 
{
    private float _mainSpeed = 100.0f;
    private float _shiftAdd = 250.0f;
    private float _maxShift = 1000.0f;
    private float _camSens = 0.25f;
    private float _totalRun = 1.0f;
    private Vector3 _lastMouse = new (255, 255, 255);
    private Vector3 _camera;
     
    private void Update () 
    {
        _lastMouse = Input.mousePosition - _lastMouse;
        _lastMouse = new Vector3(-1 * _lastMouse.y * _camSens, _lastMouse.x * _camSens, 0);
        _lastMouse = new Vector3(transform.eulerAngles.x + _lastMouse.x , transform.eulerAngles.y + _lastMouse.y, 0);
        transform.eulerAngles = _lastMouse;
        _lastMouse = Input.mousePosition;
       
        _camera = GetBaseInput();
        
        TryBustFly();
       
        _camera *= Time.deltaTime;

        TryFixTranslateCamera();
    }

    private void TryFixTranslateCamera()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 newPosition = transform.position;

            transform.Translate(_camera);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(_camera);
        }
    }

    private void TryBustFly()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _totalRun += Time.deltaTime;
            _camera *= _totalRun * _shiftAdd;
            _camera.x = Mathf.Clamp(_camera.x, -_maxShift, _maxShift);
            _camera.y = Mathf.Clamp(_camera.y, -_maxShift, _maxShift);
            _camera.z = Mathf.Clamp(_camera.z, -_maxShift, _maxShift);
        }
        else
        {
            _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
            _camera *= _mainSpeed;
        }
    }

    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0 , 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }

        return p_Velocity;
    }
}