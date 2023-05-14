using Placement;
using UnityEngine;

namespace Game
{
    public class SL_GameUI : MonoBehaviour
    {
        [SerializeField] private Rect menuRect = new Rect(0.0f, 0.0f, Screen.width * 0.3f, Screen.height);
        [SerializeField] private Rect backButtonRect = new Rect(Screen.width * 0.3f, 50.0f, 50.0f, 50.0f);
        [SerializeField] private Rect openButtonRect = new Rect(0.0f, 50.0f, 50.0f, 50.0f);
        [SerializeField] private Texture closeButton = null;
        private bool isOpen = true;

        public static bool IsOver { get; private set; } = false;

        private void OnGUI()
        {
            if (isOpen)
            {
                GUI.Window(0, menuRect, GameWindow, "Avaibles furnitures");
                if (GUI.Button(backButtonRect, closeButton))
                {
                    isOpen = false;
                }
            }

            else
            {
                if (GUI.Button(openButtonRect, closeButton))
                {
                    isOpen = true;
                }
            }
            
            IsOver = menuRect.Contains(Event.current.mousePosition);
        }
        private void GameWindow(int _windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            SL_GameItem[] _gameItems = SL_DataCenter.Instance!.Catalog;
            int _gameItemAmount = _gameItems.Length;
            float _currentWidth = 0.0f;
            
            for (int _itemIndex = 0; _itemIndex < _gameItemAmount; _itemIndex++)
            {
                Texture _texture = _gameItems[_itemIndex].Icon;
                
                if (_currentWidth + _texture.width > Screen.width * 0.3f)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    _currentWidth = 0.0f;
                }
                
                _currentWidth += _texture.width;
                if (GUILayout.Button(_texture))
                {
                    SL_PlacementManager.Instance!.CreateItem(_gameItems[_itemIndex]);
                    isOpen = false;
                }
            }
            
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}