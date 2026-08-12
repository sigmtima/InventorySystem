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
            _inventory.OnSlotCreated += AddSlot;
            _inventory.OnItemRemoved += RemoveSlot;
            foreach (var slot in slots)
            {
                slot.OnSelected += Selected;
            }
        }

        public void OnDisable()
        {
            _inventory.OnSlotCreated -= AddSlot;
            _inventory.OnItemRemoved -= RemoveSlot;
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

        public void AddSlot(InventorySlot slot, int count)
        {
            foreach (var slotUI in slots)
            {
                if (slotUI.IsBusy == false)
                {
                    slotUI.Initialize(slot, count);
                    break;
                }
            }
        }
    }
}
  

