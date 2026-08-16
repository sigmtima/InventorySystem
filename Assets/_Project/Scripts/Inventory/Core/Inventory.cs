using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class Inventory
    {
        private readonly List<InventorySlot> _inventorySlots = new();

        public IReadOnlyList<InventorySlot> Slots => _inventorySlots;
        
        public event Action<InventorySlot> OnItemRemoved;
        public event Action<int, int> OnSlotsSwapped;

        public event Action<InventorySlot> OnInventoryChanged;
        
        public int SlotsCount => _inventorySlots.Count;

        public Inventory(int slotsCount)
        {
            for (int i = 0; i < slotsCount; i++)
            {
                _inventorySlots.Add(new InventorySlot());
            }
        }

        public void AddItem(ItemData itemData, int count)
        {
            if (itemData == null)
            {
                Debug.LogError("ItemData is null.");
                return;
            }

            if (count <= 0)
                return;

            if (itemData.maxStack <= 0)
            {
                Debug.LogError("Max stack amount must be greater than zero.");
                return;
            }

            int remaining = count;

    
            foreach (var slot in _inventorySlots)
            {
                if (slot.IsEmpty || slot.ItemData != itemData)
                    continue;

                remaining = slot.Add(remaining);

                if (remaining <= 0)
                    return;
            }

            
            foreach (var slot in _inventorySlots)
            {
                if (!slot.IsEmpty)
                    continue;

                int amount = Math.Min(remaining, itemData.maxStack);

                slot.Init(itemData, amount);
           
                OnInventoryChanged?.Invoke(slot);

                remaining -= amount;

                if (remaining <= 0)
                    return;
            }
        }

        public void RemoveItem(InventorySlot slot)
        {
            if (slot == null)
                return;

            int index = _inventorySlots.IndexOf(slot);

            if (index == -1)
                return;

            slot.Clear();

            OnItemRemoved?.Invoke(slot);
        }

        public InventorySlot GetItem(int index)
        {
            return _inventorySlots[index];
        }

        public int GetSlotIndex(InventorySlot slot)
        {
            return _inventorySlots.IndexOf(slot);
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= _inventorySlots.Count)
                return;

            if (secondIndex < 0 || secondIndex >= _inventorySlots.Count)
                return;

            if (firstIndex == secondIndex)
                return;

            (
                _inventorySlots[firstIndex],
                _inventorySlots[secondIndex]
            ) = (
                _inventorySlots[secondIndex],
                _inventorySlots[firstIndex]
            );

            OnSlotsSwapped?.Invoke(firstIndex, secondIndex);
        }

        
    }
}