using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.Inventory
{
    public class EquipmentController : MonoBehaviour
    {

        private InventoryUI _inventoryUI;
        private SlotUI _currentItemSlotUI;
        [SerializeField] private Image frame;
        public event System.Action<SlotUI> OnEquipped;
        [Inject]
        public void Construct(InventoryUI inventoryUI)
        {
            _inventoryUI = inventoryUI;
            if (_inventoryUI == null)
            {
                Debug.LogError("EquipmentController: InventoryUI is null!");
                return;
            }
        }

        public void OnEnable()
        {
            if (_inventoryUI != null)
               _inventoryUI.OnSelected+= Init;
        }

        public void OnDisable()
        {
            if (_inventoryUI != null)
                _inventoryUI.OnSelected-= Init;
        }

        public void Init(SlotUI slotUI)
        {
            if (slotUI != null)
            {
                _currentItemSlotUI = slotUI;
                frame.transform.position = _currentItemSlotUI.transform.position;
            }

        }
        public void Equip()
        {
            if (_currentItemSlotUI != null)
            {
                
             OnEquipped?.Invoke(_currentItemSlotUI); 
            }

            if (_currentItemSlotUI == null)
            {
                Debug.LogError("EquipmentController: CurrentItemSlotUI is null!");
            }
        }
    }
}
