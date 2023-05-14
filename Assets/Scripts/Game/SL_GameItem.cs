using UnityEngine;

namespace Game
{
    public class SL_GameItem : MonoBehaviour
    {
        #region Fields

        [SerializeField, Header("GameItem values")] private bool isOccupied = false;
        [SerializeField] private Texture icon = null;

        #endregion

        #region Properties

        public bool IsOccupied => isOccupied;
        public Texture Icon => icon;

        #endregion
    
        #region Methods

        // Engine methods
        private void OnTriggerEnter(Collider _collider)
        {
            if (_collider.gameObject.GetComponent<SL_GameItem>())
            {
                isOccupied = true;
            }
        }
        private void OnTriggerExit(Collider _collider)
        {
            if (_collider.gameObject.GetComponent<SL_GameItem>())
            {
                isOccupied = false;
            }
        }

        // Custom methods
        public void SetPosition(Vector3 _position)
        {
            transform.position = _position;
        }
        public void UpdateRotation(Vector3 _euler)
        {
            transform.eulerAngles += _euler;
        }

        #endregion
    }
}
