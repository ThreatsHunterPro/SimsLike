// ReSharper disable All
using System;
using Game;
using Managers;
using UnityEngine;

public class SL_Cursor : SL_Singleton<SL_Cursor>
{
    #region Fields

    public event Action<Vector3> OnEditableSurface = null;
    public event Action<SL_GameItem> OnSelection = null;

    [SerializeField, Range(1.0f, 50.0f)] private float depth = 20.0f;
    [SerializeField] private LayerMask editableSurfaceLayer = 0,
                                       itemLayer = 0;
    [SerializeField] private SL_GameCamera gameCamera = null;

    #endregion
    
    #region Properties

    private bool IsValidCamera => gameCamera;
    public Vector3 CursorLocation => Input.mousePosition;

    #endregion

    #region Methods

    // Engine methods
    private void Update()
    {
        Interact(editableSurfaceLayer, OnEditableSurface, depth);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InteractWithObject(itemLayer, OnSelection, depth);
        }
    }
    private void OnDestroy()
    {
        OnEditableSurface = null;
        OnSelection = null;
    }
    
    // Custom methods
    private void Interact(LayerMask _layerMask, Action<Vector3> _callback, float _depth = 20.0f)
    {
        if (!IsValidCamera) return;
        
        Ray _ray = gameCamera.GetRayCursor(this);
        if (Physics.Raycast(_ray, out RaycastHit _hitRay, _depth, _layerMask))
        {
            _callback?.Invoke(_hitRay.point);
        }
    }
    private T InteractWithObject<T>(LayerMask _layerMask, Action<T> _callback, float _depth = 20.0f) where T : MonoBehaviour
    {
        if (!IsValidCamera) return null;
        
        Ray _ray = gameCamera.GetRayCursor(this);
        if (Physics.Raycast(_ray, out RaycastHit _hitRay, _depth, _layerMask))
        {
            T _object = _hitRay.collider.GetComponent<T>();
            _callback?.Invoke(_object);
            return _object;
        }
        
        return null;
    }

    #endregion
}