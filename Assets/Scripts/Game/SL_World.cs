// ReSharper disable All
using System;
using System.Collections;
using UnityEngine;

public class SL_World : MonoBehaviour
{
    #region Fields

    public event Action OnWorldLoaded = null;

    [SerializeField] int worldItemsLenght = 4, loadedItems = 0;
    
    #endregion

    #region Properties

    public int WorldItemsLenght => worldItemsLenght;

    #endregion

    #region Methods
    
    public void LoadWorldItem(int _max)
    {
        worldItemsLenght = _max;
    }
    public void LoadWorldItem()
    {
        loadedItems++;
    }
    public IEnumerator LoadWorld()
    {
        while (loadedItems < worldItemsLenght)
        {
            LoadWorldItem();
            yield return new WaitForSeconds(0.1f);
        }
        OnWorldLoaded?.Invoke();
    }
    
    #endregion
}