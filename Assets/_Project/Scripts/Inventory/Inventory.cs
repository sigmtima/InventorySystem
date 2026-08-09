using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class Inventory
    {
        private readonly List<InventorySlot>  _inventorySlots = new();
       public IReadOnlyList<InventorySlot> Slots => _inventorySlots;
        
        public event Action<InventorySlot> OnItemAdded;
        public event Action OnItemRemoved;
        public event Action<int, int> OnSlotsSwapped;
        public event Action OnInventoryChanged;

        public void AddItem(ItemData itemData)
        {
          InventorySlot slot = new InventorySlot();
          slot.Init(itemData, 1);
          _inventorySlots.Add(slot);
            OnItemAdded?.Invoke(slot);
            OnInventoryChanged?.Invoke();
        }

        public void RemoveItem(InventorySlot slot)
        {
            _inventorySlots.Remove(slot);
            OnItemRemoved?.Invoke();
            OnInventoryChanged?.Invoke();
        }

        public InventorySlot GetItem(int slotIndex)
        {
            return _inventorySlots[slotIndex];
        }

        public void Swap(int toIndex, int fromIndex)
        {
         (_inventorySlots[fromIndex], _inventorySlots[toIndex]) = (_inventorySlots[toIndex], _inventorySlots[fromIndex]);
         OnSlotsSwapped?.Invoke(fromIndex, toIndex);
         OnInventoryChanged?.Invoke();
        }
    }
}
