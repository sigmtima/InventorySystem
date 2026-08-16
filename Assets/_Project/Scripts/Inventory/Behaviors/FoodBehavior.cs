using _Project.Scripts.Inventory;
using UnityEngine;

namespace _Project.Scripts.Inventory.Items
{
    [CreateAssetMenu(
        fileName = "FoodBehavior",
        menuName = "Inventory/Behaviors/Food"
    )]
    public class FoodBehavior : ItemBehavior
    {
        public override void Use(ItemUseContext context, ItemData data)
        {
            var foodData = (ItemFoodData)data;
            context.Hunger.AddHunger(foodData.hungerRestore);
        }
    }
}