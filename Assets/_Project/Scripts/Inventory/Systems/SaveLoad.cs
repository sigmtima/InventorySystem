using System;
using System.Collections.Generic;

namespace _Project.Scripts.Inventory.SaveLoad
{
    [Serializable]
    public class InventorySaveData
    {
        public List<SlotSaveData> slots;
    }

    [Serializable]
    public class SlotSaveData
    {
        public int itemID;
        public int count;

        public void Init(int ID, int count)
        {
            itemID = ID;
            this.count = count;
        }
    }
}