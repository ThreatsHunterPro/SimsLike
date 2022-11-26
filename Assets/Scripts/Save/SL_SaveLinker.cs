// ReSharper disable All
using System;
using UnityEngine;

public class SL_SaveLinker : MonoBehaviour
{
    #region Fields

    public event Action<SL_SaveLinker> OnSaved = null;

    [SerializeField] SL_SavedObject savedObject = new SL_SavedObject();
    float timer = 0.0f;
    bool needCheck = false;

    #endregion

    #region Properties

    public SL_SavedObject SavedObject => savedObject;

    #endregion

    #region Methods

    // Engine methods
    private void Start() => InitLinker();
    private void Update() => CheckForSave();
    private void OnDestroy() => OnSaved = null;

    // Custom methods
    void InitLinker()
    {
        SL_SaveLoader.Instance.World.OnWorldLoaded += SaveLinker;
        SL_SaveManager.Instance.Add(this);
        OnSaved += SL_SaveManager.Instance.Save;
        transform.hasChanged = false;
    }
    void CheckForSave()
    {
        if (transform.hasChanged)
        {
            needCheck = true;
            timer = 0.0f;
            transform.hasChanged = false;
        }
        else if (!transform.hasChanged && needCheck)
        {
            timer += Time.deltaTime;
            if (timer < 0.5f) return;
            needCheck = false;
            timer = 0.0f;
            OnSaved?.Invoke(this);
        }
    }
    public void SaveLinker()
    {
        savedObject.SaveTransform(new SL_SaveTransform(transform));
    }
    
    #endregion
}