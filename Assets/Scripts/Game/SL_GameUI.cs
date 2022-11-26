// Resharper disable all
using UnityEngine;

public class SL_GameUI : MonoBehaviour
{
    #region Fields

    private Rect menuRect = new Rect(0.0f, 0.0f, Screen.width * 0.3f, Screen.height);

    #endregion

    #region Properties

    public static bool IsOver { get; private set; } = false;

    #endregion
    
    #region Methods

    // Engine methods
    private void OnGUI()
    {
        GUI.Window(0, menuRect, GameWindow, "");
        IsOver = menuRect.Contains(Event.current.mousePosition);
    }

    // Custom methods
    private void GameWindow(int _windowID)
    {
        GUILayout.Box("Avaibles furnitures");
        DrawGameItemsUI();
        GUI.DragWindow();
    }

    void DrawGameItemsUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        
        SL_GameItem[] _gameItems = SL_DataCenter.Instance?.Catalog;
        int _gameItemAmount = _gameItems.Length;
        for (int _itemIndex = 0; _itemIndex < _gameItemAmount; _itemIndex++)
        {
            if (GUILayout.Button(_gameItems[_itemIndex].Icon))
            {
                SL_PlacementManager.Instance?.CreateItem(_gameItems[_itemIndex]);
            }
        }
        
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    #endregion
}