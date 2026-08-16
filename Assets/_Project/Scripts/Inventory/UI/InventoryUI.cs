using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace _Project.Scripts.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<SlotUI> slots;
        public event System.Action<SlotUI> OnSelected;
        private Inventory _inventory;
        private DragDropController _dragDropController;

        [Inject]
        public void Construct(
            Inventory inventory,
            DragDropController dragDropController)
        {
            _inventory = inventory;
            _dragDropController = dragDropController;
            foreach (var slot in slots)
            {
                slot.GetDragDrop(_dragDropController);
            }
        }


        private void OnEnable()
        {
            if (_inventory == null)
                return;

            _inventory.OnInventoryChanged += AddSlot;
            _inventory.OnItemRemoved += RemoveSlot;
            _inventory.OnSlotsSwapped += Swap;

            foreach (var slot in slots)
                slot.OnSelected += Selected;

            Refresh();
        }
        public void AddSlot(InventorySlot slot)
        {
            Debug.Log("ADD SLOT");

            Refresh();
        }

        public void OnDisable()
        {
            _inventory.OnInventoryChanged -= AddSlot;
            _inventory.OnItemRemoved -= RemoveSlot;
            _inventory.OnSlotsSwapped -= Swap;
            foreach (var slot in slots)
            {
                slot.OnSelected -= Selected;
            }
        }

        public void Selected(SlotUI slot)
        {
            OnSelected?.Invoke(slot);
        }

        private void RemoveSlot(InventorySlot slot)
        {
            if (slot == null) return;
            foreach (var slotUI in slots.Where(slotUI => slotUI.InventorySlot == slot))
            {
                slotUI.Remove();
                return;
            }
        }
        
        private void Refresh()
        {
            if (slots.Count != _inventory.SlotsCount)
            {
                Debug.LogError(
                    $"InventoryUI: UI slots = {slots.Count}, " +
                    $"Inventory slots = {_inventory.SlotsCount}");
            }
            for (int i = 0; i < slots.Count; i++)
            {
                InventorySlot inventorySlot = _inventory.GetItem(i);
                slots[i].SetIndex(i);
                if (inventorySlot.IsEmpty)
                {
                    slots[i].Remove();
                }
                else
                {
                    slots[i].Initialize(inventorySlot, inventorySlot.Count);
                }
            }
        }
        public void Swap(int slot1, int slot2)
        {
            Refresh();
        }
    }
}
  

