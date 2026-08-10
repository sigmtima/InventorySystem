using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Inventory
{
    public class ItemData : ScriptableObject
    {
       public int itemID;
       public Sprite itemIcon;
       public enum  ItemType {Weapon, Food, Resources}
       public ItemType itemType; 
       public int maxStack;
       [SerializeReference]
       public ItemBehavior behavior;
    }
}
