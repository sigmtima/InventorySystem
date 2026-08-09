using _Project.Scripts.Core;
using _Project.Scripts.Inventory.Items;
using _Project.Scripts.Player;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        private Inventory _inventory;
        private ItemUseContext _itemUseContext;
        private PlayerInteractController _playerInteractController;

        [Inject]
        public void Construct(Inventory inventory, ItemUseContext itemUseContext,  PlayerInteractController playerInteractController)
        {
            _inventory = inventory;
            _itemUseContext = itemUseContext;
            _playerInteractController = playerInteractController;
        }

        public void OnEnable()
        {
            _playerInteractController.OnCollect += AddItem;
        }

        public void OnDisable()
        {
            _playerInteractController.OnCollect -= AddItem;
        }

        public void AddItem(ItemData itemData)
        {
            _inventory.AddItem(itemData);
        }
        
        public void UseItem(int slotIndex)
        {
            var  slot = _inventory.GetItem(slotIndex);
            if (slot.Count > 0)
            {
                slot.ItemData.behavior.Use(_itemUseContext, slot.ItemData);
                    slot.Remove(1);
                    if (slot.IsEmpty)
                    {
                        _inventory.RemoveItem(slot);
                    }
                
            }
        }
    }
}
