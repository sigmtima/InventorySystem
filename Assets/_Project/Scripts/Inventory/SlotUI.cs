using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        private bool _isSelected;
        public event System.Action OnSelected;
        [SerializeField] private Image image;
        private Sprite _itemIcon;
       public bool IsBusy { get;  private set; } = false;
        
        public void OnClick()
        {
            _isSelected = !_isSelected;
            if (_isSelected)
            {
                OnSelected?.Invoke();
            }
        }

        public void Render(InventorySlot slot)
        {
            image.sprite = slot.ItemData.itemIcon;
            _itemIcon = slot.ItemData.itemIcon;
            IsBusy = true;
        }

        public void UnRender()
        {
            image.sprite = null;
            _itemIcon = null;
            IsBusy = false;
        }
    }
}
