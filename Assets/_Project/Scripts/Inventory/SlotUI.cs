using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        private bool _isSelected;
        private InventorySlot _itemSlot;
        public event System.Action OnSelected;
        [SerializeField] private Image image;
         [SerializeField] private TMPro.TextMeshProUGUI itemCount;
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
        }
        public void ChangeText(int count)
        {
            
        }
        public void OnDisable()
        {
            _itemSlot.OnSlotChanged-=ChangeText;
        }
    }
  
}
