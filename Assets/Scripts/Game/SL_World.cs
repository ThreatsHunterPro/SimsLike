using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class SL_World : MonoBehaviour
    {
        public event Action OnWorldLoaded = null;
        [SerializeField] int worldItemsLenght = 4, loadedItems = 0;

        private void LoadWorldItem()
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
    }
}