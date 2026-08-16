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

            if (targetSlot == null)
                return;

            if (_draggedSlot == targetSlot)
                return;

            _inventory.Swap(
                _draggedSlot.Index,
                targetSlot.Index
            );
        }
        public void EndDrag()
        {
            dragIcon.gameObject.SetActive(false);
            _draggedSlot = null;
        }
    }
}
