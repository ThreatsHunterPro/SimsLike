using UnityEngine;

namespace Managers
{
    public abstract class SL_Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        public static T Instance => instance;

        #region Methods

        // Engine methods
        protected virtual void Awake() => InitInstance();

        // Custom methods
        private void InitInstance()
        {
            if (instance)
            {
                Destroy(instance);
                instance = null;
            }

            instance = this as T;
            name = $"{name} [MANAGER]";
        }

        #endregion
    }
}