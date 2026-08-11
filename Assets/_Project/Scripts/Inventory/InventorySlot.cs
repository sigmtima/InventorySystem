using System;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventorySlot
    {
        public ItemData ItemData { get; private set; }
        public int Count { get; private set; }
        
        public bool IsEmpty { get; private set; }
        public event Action<int> OnSlotChanged;

        public void Init(ItemData itemData, int count)
        {
            IsEmpty = false;
            ItemData = itemData;
            Count = count;
        }
        
        public int Add(int amount)
            {
                int total = Count + amount;
                int max = ItemData.maxStack;

                Count = Math.Min(total, max); 
                OnSlotChanged?.Invoke(Count);
                return Math.Max(0, total - max); 
               
            }
            
        public void Remove(int amount)
        {
            Count = Mathf.Max(0, Count - amount);
            if (Count == 0)
            {
                IsEmpty = true;
            }
            OnSlotChanged?.Invoke(Count);
        }
        
    }
}
