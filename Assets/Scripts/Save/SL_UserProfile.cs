// ReSharper disable All
using UnityEngine;

public struct SL_UserProfile
{
    #region Fields

    [SerializeField] string userID;
    [SerializeField] SL_SaveEnvironment save;
    
    #endregion

    #region Properties

    public string UserProfileName => $"Profile_{userID}";
    public SL_SaveEnvironment Save => save;

    #endregion

    #region Methods
    
    public SL_UserProfile(string _id)
    {
        userID = _id;
        save = new SL_SaveEnvironment();
    }
    public void Init()
    {
        save = new SL_SaveEnvironment(this);
    }
    
    #endregion
}