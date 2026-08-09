using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventorySlot
    {
        public ItemData ItemData { get; private set; }
        public int Count { get; private set; }
        
        public bool IsEmpty { get; private set; }

        public void Init(ItemData itemData, int count)
        {
            ItemData = itemData;
            Count = count;
        }
        
        public void Add(int amount)
        {
            Count += amount;
        }

        public void Remove(int amount)
        {
            Count -= amount;
            if (Count <= 0)
            {
                IsEmpty = true;
            }
        }
        
    }
}
