using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        private bool _isSelected;
        private InventorySlot _itemSlot;
        public event System.Action<SlotUI> OnSelected;
        [SerializeField] private Image image;
         [SerializeField] private TMPro.TextMeshProUGUI itemCount;
        private Sprite _itemIcon;
        public InventorySlot InventorySlot => _itemSlot;
       public bool IsBusy { get;  private set; } = false;
        public void OnClick()
        {
            _isSelected = !_isSelected;
            if (_isSelected)
            {
                OnSelected?.Invoke(this);
            }
        }

        public void Render(InventorySlot slot, int count)
        {
            image.sprite = slot.ItemData.itemIcon;
            _itemIcon = slot.ItemData.itemIcon;
            itemCount.text = count.ToString();
            IsBusy = true;
        }

        public void Initialize(InventorySlot slot, int count)
        {
            _itemSlot = slot;
            Render(slot, count);
            _itemSlot.OnSlotChanged += ChangeText;
        }

        public void UnRender()
        {
            image.sprite = null;
            _itemIcon = null;
            itemCount.text = "";
            IsBusy = false;
            _itemSlot.OnSlotChanged -= ChangeText;
        }
        public void ChangeText(int count)
        {
            itemCount.text = count.ToString();
        }
        public void OnDisable()
        {
            if (_itemSlot != null)
            {
                _itemSlot.OnSlotChanged -= ChangeText;
            }
        }
    }
  
}
