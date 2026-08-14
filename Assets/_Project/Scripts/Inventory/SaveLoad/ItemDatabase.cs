using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Inventory.SaveLoad
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemData> allItems = new();

        public ItemData GetItemById(int id)
        {
            return allItems.FirstOrDefault(item => item.itemID == id);
        }
    }
}
