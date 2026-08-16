using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory
{
    public class SlotUI : MonoBehaviour, IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,  IPointerClickHandler
    {
    
        private bool _isSelected;
        private InventorySlot _itemSlot;
     
        public event System.Action<SlotUI> OnSelected;
        [SerializeField] private Image image;
         [SerializeField] private TMPro.TextMeshProUGUI itemCount;
        public InventorySlot InventorySlot => _itemSlot;
        private DragDropController _dragDropController;
       public bool IsBusy { get;  private set; } = false;
       public int Index { get; private set; }

       public void OnPointerClick(PointerEventData eventData)
       {
           OnClick();
       }
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
            if (slot == null)
            {
                Debug.LogError("SlotUI.Render: slot is null");
                return;
            }

            if (slot.ItemData == null)
            {
                Debug.LogError("SlotUI.Render: ItemData is null");
                return;
            }

            if (image == null)
            {
                Debug.LogError("SlotUI.Render: image is not assigned");
                return;
            }

            image.sprite = slot.ItemData.itemIcon;
            itemCount.text = count.ToString();
            IsBusy = true;
        }

       
        public void Initialize(
            InventorySlot slot,
            int count
            )
        {
            if (_itemSlot != null)
                _itemSlot.OnSlotChanged -= ChangeText;

            _itemSlot = slot;

            if (_itemSlot == null)
            {
                Remove();
                return;
            }

            Render(slot, count);
            
            _itemSlot.OnSlotChanged += ChangeText;
            Debug.Log("ПРЕДМЕТ ДОБАВЛЕН");
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
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsBusy)
                return;
            Debug.Log("Drag Started");

            // Запоминаем, что именно тащим
            _dragDropController.StartDrag(this);
        }
        public void OnDrag(PointerEventData eventData)
        {
            _dragDropController.Drag(eventData.position);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            _dragDropController.EndDrag();
        }
        public void OnDrop(PointerEventData eventData)
        {
            _dragDropController.Drop(this);
        }

        public void GetDragDrop(DragDropController dragDrop)
        {
            _dragDropController = dragDrop;
        }
        public void SetIndex(int index)
        {
            Index = index;
        }
    }
  
}
