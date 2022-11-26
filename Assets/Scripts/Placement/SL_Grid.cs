// ReSharper disable All
using UnityEngine;

public class SL_Grid : MonoBehaviour
{
    #region Fields

    [SerializeField, Header("Grid values")] private MeshRenderer surface = null;
    [SerializeField, Range(0.0f, 10.0f)] private float snapValue = 0.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float pointSize = 0.1f;
    [SerializeField] private Color pointColor = Color.blue;

    #endregion

    #region Properties

    public bool IsValidSurface => surface;
    public Vector3 Extents => IsValidSurface ? surface.bounds.extents : Vector3.zero;

    #endregion
    
    #region Methods

    // Engine methods
    private void OnDrawGizmosSelected() => DrawGrid();
    
    // Custom methods
    private void DrawGrid()
    {
        if (!IsValidSurface) return;

        for (float _i = -Extents.x; _i < Extents.x; _i++)
        {
            for (float _j = -Extents.z; _j < Extents.z; _j++)
            {
                Vector3 _gridPoint = new Vector3(_i, 0.1f, _j);
                Gizmos.color = pointColor;
                Gizmos.DrawSphere(_gridPoint, pointSize);
            }
        }
    }
    public Vector3 GetSnapPosition(Vector3 _hitPosition)
    {
        float _x = Mathf.RoundToInt(_hitPosition.x) + snapValue;
        float _xLimit = Extents.x;
        _x = Mathf.Clamp(_x, -_xLimit + 1, _xLimit - 1);
        
        float _z = Mathf.RoundToInt(_hitPosition.z) + snapValue;
        float _zLimit = Extents.z;
        _z = Mathf.Clamp(_z, -_zLimit + 1, _zLimit - 1);

        return new Vector3(_x, _hitPosition.y, _z);
    }

    #endregion
}