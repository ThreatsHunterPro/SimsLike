// ReSharper disable All
using System;
using System.IO;

public class SL_SaveEnvironment
{
    #region Fields

    public event Action<SL_SaveEnvironment> OnEnvironmentLoaded = null; 

    const string GAME_NAME = "SimsProject",
        SAVE_FOLDER = "SaveFolder",
        SAVE_FILE = "savefile",
        SAVE_EXT = ".privateextension";
    
    #endregion

    #region Properties

    public string PathSaveFileDirectory { get; }
    public string PathSaveFile { get; }

    public bool DirectoryExist => Directory.Exists(PathSaveFileDirectory);
    public bool FileExist => File.Exists(PathSaveFile);

    #endregion

    #region Methods
    
    public SL_SaveEnvironment()
    {
        PathSaveFileDirectory = "";
        PathSaveFile = "";
    }
    public SL_SaveEnvironment(SL_UserProfile _profile)
    {
        string _rootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string _profileName = _profile.UserProfileName;
        PathSaveFileDirectory = Path.Combine(_rootPath, GAME_NAME, _profileName, SAVE_FOLDER);
        if (!DirectoryExist)
        {
            Directory.CreateDirectory(PathSaveFileDirectory);
        }
        PathSaveFile = Path.Combine(PathSaveFileDirectory, SAVE_FILE + SAVE_EXT);
        OnEnvironmentLoaded?.Invoke(this);
    }

    #endregion
}