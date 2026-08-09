using _Project.Scripts.Inventory;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public interface IDamageable
    {
        public void TakeDamage(int amount);
    }
    public interface IHealable
    {
        void Heal(int amount);
    }

    public interface IItem
    {
        public void Use(ItemUseContext context, ItemData data);
    }

    public interface ICollectible
    {
         ItemData Collect(); 
    }
}
