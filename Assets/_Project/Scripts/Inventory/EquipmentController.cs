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
        }

        public void OnEnable()
        {
            _inventoryUI.OnSelected+= Init;
        }

        public void OnDisable()
        {
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
            
        }
    }
}
