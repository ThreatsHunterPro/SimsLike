// ReSharper disable All
using System;
using System.Reflection;
using UnityEngine;

public class SL_GameCamera : MonoBehaviour
{
    #region Fields

    public event Action<GameObject> OnWallDetected = null;

    [SerializeField, Header("GameCamera values")] private bool invertRotation = false;
    [SerializeField, Range(0.0f, 360.0f)] private float orbitAngle = 0.0f,
                                                        radius = 20.0f, 
                                                        height = 5.0f;
    [SerializeField, Range(0.0f, 100.0f)] private float rotateSpeed = 2.0f;
    [SerializeField, Range(0.0f, 100.0f)] private float customTickRate = 0.2f;
    [SerializeField, Range(0.0f, 100.0f)] private float depth = 20.0f;
    [SerializeField] private LayerMask wallLayer = 0;
    [SerializeField] private Camera sensorCamera = null;
    [SerializeField] private Transform target = null;
    private GameObject lastWall = null;

    #endregion
    
    #region Properties

    public bool IsValid => target;
    public Camera SensorCamera => sensorCamera;
    public bool InvertRotation => invertRotation;

    #endregion

    #region Methods

    // Engine methods
    private void Start()
    {
        InitCamera();
        InvokeRepeating(nameof(WallDetection), 0.0f, customTickRate);
    }
    private void LateUpdate()
    {
        CameraOrbitRotation();
        CameraOrbitLookAt();
    }
    private void OnDestroy()
    {
        OnWallDetected = null;
    }
    
    // Custom methods
    private void InitCamera()
    {
        OnWallDetected += HideWall;
    }
    private void CameraOrbitRotation()
    {
        if (!IsValid) return;
        
        orbitAngle = GetAngle(orbitAngle, Input.GetKey(KeyCode.Mouse1), Input.GetAxis("Mouse X"));
        float _rad = Mathf.Deg2Rad * orbitAngle;
        float _x = Mathf.Cos(_rad) * radius;
        float _z = Mathf.Sin(_rad) * radius;
        sensorCamera.transform.position = new Vector3(_x, height, _z) + target.position;
    }
    private void CameraOrbitLookAt()
    {
        if (!IsValid) return;
        
        Transform _transform = sensorCamera.transform;
        _transform.rotation = Quaternion.LookRotation(target.position - _transform.position);
    }
    private float GetAngle(float _angle, bool _action, float _axis)
    {
        if (!_action) return _angle;
        
        _angle += _axis * rotateSpeed;
        _angle %= 360.0f;
        return _angle;
    }
    private void WallDetection()
    {
        if (!IsValid) return;
        
        Ray _raycamera = sensorCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(_raycamera, out RaycastHit _hit, depth, wallLayer))
        {
            OnWallDetected?.Invoke(_hit.collider.gameObject);
        }
    }
    private void HideWall(GameObject _wall)
    {
        if (!_wall) return;

        if (lastWall && _wall.GetInstanceID() != lastWall.GetInstanceID())
        {
            lastWall.SetActive(true);
        }
        
        lastWall = _wall;
        lastWall.SetActive(false);
    }
    public Ray GetRayCursor(SL_Cursor _cursor) => sensorCamera.ScreenPointToRay(_cursor.CursorLocation);

    #endregion
}