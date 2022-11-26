// ReSharper disable All
using System;
using System.Collections;
using UnityEngine;

public class SL_SaveLoader : SL_Singleton<SL_SaveLoader>
{
    #region Fields

    public event Action<float> OnLoadUpdate = null;
    public event Action OnLoadEnd = null;
    
    [SerializeField] SL_World world = null;
    [SerializeField] float loadPercent = 0.0f, currentLoad = 0.0f, maxPercentLoad = 2.0f;
    
    #endregion

    #region Properties

    public SL_World World => world;

    #endregion

    #region Methods
    
    // Engine methods
    IEnumerator Start()
    {
        world.OnWorldLoaded += UpdateLoad;
        SL_SaveManager.Instance.OnSaveLoaded += UpdateLoad;
        yield return world.LoadWorld();
        yield return SL_SaveManager.Instance.LoadSaveFile();
        yield return new WaitForSeconds(1.0f);
        OnLoadEnd?.Invoke();
    }
    private void OnDestroy()
    {
        OnLoadUpdate = null;
    }
    
    // Custom methods
    void UpdateLoad()
    {
        currentLoad++;
        loadPercent = (currentLoad / maxPercentLoad) * 100.0f;
        OnLoadUpdate?.Invoke(loadPercent);
    }
    
    #endregion
}