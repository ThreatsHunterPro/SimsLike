// ReSharper disable All
using UnityEngine;

public class SL_DataCenter : SL_Singleton<SL_DataCenter>
{
    [SerializeField] private SL_GameItem[] catalog = null;
    
    public SL_GameItem[] Catalog => catalog;
}
