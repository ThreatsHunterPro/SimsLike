// ReSharper disable All
using UnityEngine;

[System.Serializable]
public struct SL_CustomVector3
{
    #region Fields

    public float x;
    public float y;
    public float z;
    
    #endregion

    #region Properties

    public float X => x;
    public float Y => y;
    public float Z => z;

    #endregion

    #region Methods
    
    public SL_CustomVector3(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
    public SL_CustomVector3(Vector3 _vector)
    {
        x = _vector.x;
        y = _vector.y;
        z = _vector.z;
    }

    #endregion
}
    
[System.Serializable]
public class SL_SaveTransform
{
    #region Fields

    [SerializeField] SL_CustomVector3 position, rotation, scale;

    #endregion

    #region Properties

    public SL_CustomVector3 Position => position;
    public SL_CustomVector3 Rotation => rotation;
    public SL_CustomVector3 Scale => scale;

    #endregion

    #region Methods

    public SL_SaveTransform(Transform _transform)
    {
        if (!_transform)
        {
            position = new SL_CustomVector3(0.0f, 0.0f, 0.0f);
            rotation = new SL_CustomVector3(0.0f, 0.0f, 0.0f);
            scale = new SL_CustomVector3(0.0f, 0.0f, 0.0f);
            return;
        }
        position = new SL_CustomVector3(_transform.position);
        rotation = new SL_CustomVector3(_transform.eulerAngles);
        scale = new SL_CustomVector3(_transform.localScale);
    }

    #endregion
}