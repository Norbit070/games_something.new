using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Vector3 _positionOffset = Vector3.up;
    [SerializeField] private float _rotationSpeed = 65f;
    [SerializeField] private float _zoomSpeed = 10f;
    [SerializeField] private float _mouseZoomMultiplier = 3f;
    [SerializeField] private float _minZoom = 3f;
    [SerializeField] private float _maxZoom = 14f;
    private float _currentZoom;
    
    void Start()
    {
        Init();
        
    }
    private void Init()
    {
        // Вычисляем текущий зум камеры
        // Через расстояние между целью и трансформой камеры
        _currentZoom = (_target.position - _cameraTransform.position).magnitude;
    }
    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }
    private void MoveCamera()
    {
        if (!_target || !_cameraRoot)
        {
           
            return;
        }
        Vector3 targetPosition = _target.position + _positionOffset;
        _cameraRoot.transform.position = Vector3.Lerp(_cameraRoot.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
    }
    private void RotateCamera()
    {
        if (!_cameraRoot)
        {
            return;
        }
        float direction = 0;

        if (Input.GetKey(KeyCode.Q))
        {
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.E)) 
        {
            direction = -1;
        }
        if (Mathf.Approximately(direction, 0))
        {
            
            return;
        }
        Vector3 cameraEuler = _cameraRoot.eulerAngles;
        cameraEuler.y += direction * _rotationSpeed * Time.deltaTime;
        _cameraRoot.eulerAngles = cameraEuler;
    }
    private void ZoomCamera()
    {
        if (!_cameraTransform)
        {
            return;
        }
        float direction = 0;

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadMinus))
        {
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.KeypadPlus))
        {
            direction = -1;
        }
        else if (!Mathf.Approximately(Input.mouseScrollDelta.y, 0))
        {
            direction = -Input.mouseScrollDelta.y * _mouseZoomMultiplier;
        }

        if (Mathf.Approximately(direction, 0))
        {
            return;
        }
        _currentZoom += direction * _zoomSpeed * Time.deltaTime;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);
        _cameraTransform.position = _cameraRoot.position - _cameraTransform.forward * _currentZoom;
    }

    
    void Update()
    {
        
    }
}
