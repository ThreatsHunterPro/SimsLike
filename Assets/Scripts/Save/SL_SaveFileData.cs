// ReSharper disable All
using UnityEngine;

public struct SL_SaveFileData
{
    [SerializeField] SL_SavedObject[] savedObjects;

    public SL_SavedObject[] SavedObjects => savedObjects;

    public SL_SaveFileData(SL_SavedObject[] _savedObjects)
    {
        savedObjects = _savedObjects;
    }
}