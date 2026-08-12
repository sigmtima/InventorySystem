
namespace _Project.Scripts.Inventory.Items{
    public class FoodBehavior : ItemBehavior
    {
        public override void Use(ItemUseContext context, ItemData data)
        {
            var foodData = (ItemFoodData)data;
            context.Hunger.AddHunger(foodData.hungerRestore);
        }
    }
}
