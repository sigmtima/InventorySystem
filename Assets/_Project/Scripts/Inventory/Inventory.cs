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
        
        public event Action<InventorySlot, int> OnItemAdded;
        public event Action OnItemRemoved;
        public event Action<int, int> OnSlotsSwapped;


        public void AddItem( ItemData itemData, int count)
        {
            int remaining = count;


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
                OnItemAdded?.Invoke(inventorySlot, amount);
                _inventorySlots.Add(inventorySlot);

                remaining -= amount;
            }
        }

        public void RemoveItem(InventorySlot slot)
        {
            _inventorySlots.Remove(slot);
            OnItemRemoved?.Invoke();

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
