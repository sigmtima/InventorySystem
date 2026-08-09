using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public abstract class ItemBehavior
    {
        public abstract void Use(ItemUseContext context, ItemData data);
    }
}
