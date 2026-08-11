using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace _Project.Scripts.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<SlotUI> slots;
        public event System.Action<SlotUI> OnSelected;
        private Inventory _inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }


        public void OnEnable()
        {
            _inventory.OnItemAdded += AddSlot;
            foreach (var slot in slots)
            {
                slot.OnSelected += Selected;
            }
        }

        public void OnDisable()
        {
            _inventory.OnItemAdded -= AddSlot;
            foreach (var slot in slots)
            {
                slot.OnSelected -= Selected;
            }
        }

        public void Selected(SlotUI slot)
        {
            OnSelected?.Invoke(slot);
        }

        public void AddSlot(InventorySlot slot, int count)
        {
            foreach (var slotUI in slots)
            {
                if (slotUI.IsBusy == false)
                {
                    slotUI.Initialize(slot, count);
                    break;
                }
            }
        }
    }
}
  

