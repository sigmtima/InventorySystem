using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public abstract class ItemBehavior : ScriptableObject
    {
        public abstract void Use(ItemUseContext context, ItemData data);
    }
}