// ReSharper disable All
using UnityEngine;

public struct SL_SavedObject
{
    #region Fields

    [SerializeField] int id;
    [SerializeField] SL_SaveTransform transform;
    
    #endregion

    #region Properties

    public int ID => id;

    #endregion

    #region Methods
    
    public void SaveTransform(SL_SaveTransform _transform)
    {
        transform = _transform;
    }
    public void LoadTransform(Transform _transform)
    {
        _transform.position = new Vector3(transform.Position.X, transform.Position.Y, transform.Position.Z);
        _transform.position = new Vector3(transform.Rotation.X, transform.Rotation.Y, transform.Rotation.Z);
        _transform.position = new Vector3(transform.Scale.X, transform.Scale.Y, transform.Scale.Z);
    }

    #endregion
}