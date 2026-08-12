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
            itemCount.text = count.ToString();
            IsBusy = true;
        }

        public void Initialize(InventorySlot slot, int count)
        {
            if (_itemSlot != null)
                _itemSlot.OnSlotChanged -= ChangeText;

            _itemSlot = slot;

            Render(slot, count);

            if (_itemSlot != null)
                _itemSlot.OnSlotChanged += ChangeText;
        }
        public void UnRender()
        {
            image.sprite = null;
            itemCount.text = "";
            IsBusy = false;
            if (_itemSlot != null)
            {
                _itemSlot.OnSlotChanged -= ChangeText;
            }
        }

        public void Remove()
        {  
            UnRender();
            _itemSlot = null;
          
        }
        
        public void ChangeText(int count)
        {
            itemCount.SetText("{0}", count);
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
