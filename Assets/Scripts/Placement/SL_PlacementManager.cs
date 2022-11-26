// ReSharper disable All
using UnityEngine;

public class SL_PlacementManager : SL_Singleton<SL_PlacementManager>
{
    #region Fields

    [SerializeField, Header("PlacementManager values"), Range(0.0f, 100.0f)] private float rotateSpeed = 20.0f;
    [SerializeField] private SL_Grid grid = null;
    [SerializeField] private Transform root = null;
    private SL_GameItem currentItem = null;

    Vector3 hitPos = Vector3.zero;
    
    #endregion

    #region Properties

    private bool IsValidGrid => grid;
    private bool IsValidRoot => root;
    private Vector3 Rotation => Vector3.up * rotateSpeed / 50.0f;

    #endregion
    
    #region Methods

    // Engine methods
    private void Start()
    { 
        SL_Cursor.Instance.OnEditableSurface += SetPosition;
        SL_Cursor.Instance.OnSelection += SetItem;
    }
    private void Update()
    {
        // if (SL_GameUI.IsOver) return;
        
        RotateItem();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            DropItem();
        }
        // WrongSelectorCoolDown();
    }
    private void OnDrawGizmos()
    {
        if (!currentItem) return;
        Gizmos.color = currentItem.IsOccupied ? Color.red : Color.green;
        Gizmos.DrawSphere(currentItem.transform.position + Vector3.up * 3.0f, 0.25f);
    }
    
    // Custom methods
    public void CreateItem(SL_GameItem _item)
    {
        if (currentItem)
        {
            DestroyCurrentItem();
        }
        
        currentItem = Instantiate(_item, IsValidRoot ? root : null);
    }
    private void DestroyCurrentItem()
    {
        Destroy(currentItem.gameObject);
    }
    private void SetItem(SL_GameItem _item)
    {
        if (!_item) return;
        currentItem = _item;
    }
    
    private void SetPosition(Vector3 _position)
    {
        if (!IsValidGrid || !currentItem) return;
        currentItem.SetPosition(grid.GetSnapPosition(_position));
    }
    private void RotateItem()
    {
        if (!currentItem) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentItem.UpdateRotation(Rotation);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentItem.UpdateRotation(-Rotation);
        }
    }
    private void DropItem()
    {
        if (!currentItem || currentItem.IsOccupied) return;
        currentItem = null;
    }
    
    #endregion
}