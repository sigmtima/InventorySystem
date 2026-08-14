using System.Collections.Generic;
using System.Linq;
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

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }


        public void OnEnable()
        {
          
            _inventory.OnItemRemoved += RemoveSlot;
            _inventory.OnSlotsSwapped += Swap;
           
            foreach (var slot in slots)
            {
                slot.OnSelected += Selected;
            }
            Refresh();
        }

        public void OnDisable()
        {
           
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
            for (int i = 0; i < slots.Count; i++)
            {
                InventorySlot inventorySlot = _inventory.GetItem(i);

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
  

