using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.Inventory
{
    public class DragDropController : MonoBehaviour
    {
       
        private SlotUI _draggedSlot;
        [SerializeField] private Image dragIcon;
        private Inventory _inventory;
        
        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }
        
        public void StartDrag(SlotUI slot)
        {
            _draggedSlot = slot;

            dragIcon.sprite = slot.InventorySlot.ItemData.itemIcon;
            dragIcon.gameObject.SetActive(true);
        }
        public void Drag(Vector2 mousePosition)
        {
            if (_draggedSlot == null)
                return;

            dragIcon.transform.position = mousePosition;
        }
        public void Drop(SlotUI targetSlot)
        {
            if (_draggedSlot == null)
                return;

            if (_draggedSlot == targetSlot)
                return;
            
            int fromIndex = _inventory.GetSlotIndex(_draggedSlot.InventorySlot);
            int toIndex = _inventory.GetSlotIndex(targetSlot.InventorySlot);

            _inventory.Swap(fromIndex, toIndex);
        }
        public void EndDrag()
        {
            dragIcon.gameObject.SetActive(false);
            _draggedSlot = null;
        }
    }
}
