 using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class Inventory
    {
        private readonly List<InventorySlot>  _inventorySlots = new();
       public IReadOnlyList<InventorySlot> Slots => _inventorySlots;
        
        public event Action<InventorySlot, int> OnSlotCreated;
        public event Action<InventorySlot> OnItemRemoved;
        public event Action<int, int> OnSlotsSwapped;


        public void AddItem( ItemData itemData, int count)
        {
            if (count <= 0)
                return;
            int remaining = count;

            if (itemData.maxStack <= 0)
            {
                Debug.LogError("Max stack amount must be greater than zero");
                return;
            }
            foreach (var inventorySlot in _inventorySlots)
            {
                if (inventorySlot.ItemData != itemData)
                    continue;

                if (remaining <= 0)
                    break;

                remaining = inventorySlot.Add(remaining);
            }


            while (remaining > 0)
            {
                int amount = Math.Min(remaining, itemData.maxStack);

                var inventorySlot = new InventorySlot();
                inventorySlot.Init(itemData, amount);
                _inventorySlots.Add(inventorySlot);
                OnSlotCreated?.Invoke(inventorySlot, amount);

                remaining -= amount;
            }
        }

        public void RemoveItem(InventorySlot slot)
        {
            if (slot == null)
                return;
            if (_inventorySlots.Remove(slot))
            {
                OnItemRemoved?.Invoke(slot);
            }
        }

        public InventorySlot GetItem(int slotIndex)
        {
            return _inventorySlots[slotIndex];
        }

        public void Swap(int toIndex, int fromIndex)
        {
         (_inventorySlots[fromIndex], _inventorySlots[toIndex]) = (_inventorySlots[toIndex], _inventorySlots[fromIndex]);
         OnSlotsSwapped?.Invoke(fromIndex, toIndex);
        }
    }
}
