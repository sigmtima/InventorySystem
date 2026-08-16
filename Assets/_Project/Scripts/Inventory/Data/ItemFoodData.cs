using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemFoodData", menuName = "Inventory/ItemFoodData")]
    public class ItemFoodData : ItemData
    {
      public int hungerRestore;

    }
}

