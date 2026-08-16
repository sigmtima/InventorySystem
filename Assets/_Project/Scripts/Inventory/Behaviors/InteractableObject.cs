using _Project.Scripts.Core;
using UnityEngine;

namespace _Project.Scripts.Inventory.Items
{
    public class InteractableObject : MonoBehaviour, ICollectible
    {
        [SerializeField] private ItemData data;
        [SerializeField] private int count;
        
        public ItemData CollectData()
        {
            return data;
        }

        public int CollectCount()
        {
            return count;
        }
    }
}
