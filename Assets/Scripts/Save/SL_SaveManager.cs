// ReSharper disable All
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SL_SaveManager : SL_Singleton<SL_SaveManager>
{
    #region Fields

    public event Action OnNewSave = null, OnSaveLoaded = null;

    Dictionary<int, SL_SaveLinker> savedItems = new Dictionary<int, SL_SaveLinker>();
    [SerializeField] SL_UserProfile userProfile = new SL_UserProfile();
    [SerializeField] SL_SaveBinary<SL_SaveFileData> binary = new SL_SaveBinary<SL_SaveFileData>();
    
    #endregion

    #region Methods
    
    // Engine methods
    protected override void Awake()
    {
        base.Awake();
        userProfile.Init();
        OnNewSave += WriteSaveFile;
    }
    private void OnDestroy()
    {
        OnNewSave = null;
        OnSaveLoaded = null;
    }
    
    // Custom methods
    public void Add(SL_SaveLinker _linker)
    {
        if (savedItems.ContainsKey(_linker.SavedObject.ID)) return;
        savedItems.Add(_linker.SavedObject.ID, _linker);
        _linker.name += $" [SAVED {_linker.SavedObject.ID}]";
    }
    public void Save(SL_SaveLinker _linker)
    {
        if (!savedItems.ContainsKey(_linker.SavedObject.ID)) return;
        savedItems[_linker.SavedObject.ID].SaveLinker();
        OnNewSave?.Invoke();
    }
    SL_SaveFileData GetSaveFile()
    {
        SL_SavedObject[] _data = savedItems.Select((k) => k.Value.SavedObject).ToArray();
        return new SL_SaveFileData(_data);
    }
    void WriteSaveFile()
    {
        if (!userProfile.Save.FileExist)
            userProfile.Init();
        try
        {
            binary.Save(GetSaveFile(), userProfile.Save.PathSaveFile);
        }
        catch (IOException _exception)
        {
            Debug.LogException(_exception);
        }
    }
    public IEnumerator LoadSaveFile()
    {
        if (!userProfile.Save.DirectoryExist) yield break;
        SL_SaveFileData _saveFile = binary.Load(userProfile.Save.PathSaveFile);
        for (int _i = 0; _i < _saveFile.SavedObjects.Length; _i++)
        {
            SL_SaveLinker _linker = savedItems[_saveFile.SavedObjects[_i].ID];
            if (!_linker) continue;
            _saveFile.SavedObjects[_i].LoadTransform(_linker.transform);
            yield return new WaitForSeconds(0.5f);
        }
        OnSaveLoaded?.Invoke();
    }
    
    #endregion
}