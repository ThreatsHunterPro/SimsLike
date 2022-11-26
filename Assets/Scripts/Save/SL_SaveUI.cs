// ReSharper disable All
using UnityEngine;

public class SL_SaveUI : MonoBehaviour
{
    #region Fields

    [SerializeField] GUISkin skin = null;
    [SerializeField] bool isLoadedUI = false;
    float loadingBarPercent = 0.0f;
    
    #endregion

    #region Properties

    public bool IsValid => skin;

    #endregion

    #region Methods
    
    // Engine methods
    private void Start()
    {
        SL_SaveLoader.Instance.OnLoadUpdate += UpdateLoadingBarUI;
        SL_SaveLoader.Instance.OnLoadEnd += SetLoaded;
    }
    private void OnGUI()
    {
        if (!IsValid || isLoadedUI) return;
        
        GUI.skin = skin;
        GUI.color = Color.black;
        GUI.Box(new Rect(0.0f, 0.0f, Screen.width, Screen.height), ""); 
        GUI.color = Color.white;
        GUI.Label(new Rect(Screen.width / 2.0f, Screen.height / 2.0f, 500.0f, 500.0f), $"Loading : { loadingBarPercent}%");
    }
    
    // Custom methods
    void SetLoaded() => isLoadedUI = true;
    void UpdateLoadingBarUI(float _loadingPercent) => loadingBarPercent = _loadingPercent;
    
    #endregion
}