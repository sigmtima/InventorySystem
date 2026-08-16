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
        private EquipmentController _equipmentController;

        [Inject]
        public void Construct(Inventory inventory, ItemUseContext itemUseContext,
            PlayerInteractController playerInteractController, EquipmentController equipmentController)
        {
            _equipmentController = equipmentController;
            _inventory = inventory;
            _itemUseContext = itemUseContext;
            _playerInteractController = playerInteractController;
        }

        public void OnEnable()
        {
            if (_playerInteractController == null)
            {
                Debug.LogError("InventoryController: PlayerInteractController is null!");
                return;
            }

            if (_equipmentController == null)
            {
                Debug.LogError("InventoryController: EquipmentController is null!");
                return;
            }
            _playerInteractController.OnCollect += AddItem;
            _equipmentController.OnEquipped += UseItem;
        }

        public void OnDisable()
        {
            _playerInteractController.OnCollect -= AddItem;
            _equipmentController.OnEquipped -= UseItem;

        }

        public void AddItem(ItemData itemData, int count)
        {
            _inventory.AddItem(itemData, count);
        }

        public void UseItem(SlotUI slotUI)
        {
            if (slotUI != null)
            {
                InventorySlot slot = slotUI.InventorySlot;
                if (slot != null)
                {
                    if (slot.Count > 0)
                    {
                        if(slot.ItemData.behavior != null)
                        {
                            slot.ItemData.behavior.Use(_itemUseContext, slot.ItemData);
                            if (slot.ItemData.consumeOnUse == true)
                            { 
                                slot.Remove(slot.ItemData.consumeAmount);
                            }

                            if (slot.IsEmpty)
                            {
                                _inventory.RemoveItem(slot);
                            }
                        }
                    }
                }
            }
        }
    }
}

        
            
        
