using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Inventory.SaveLoad
{
    public class SaveLoadManager : MonoBehaviour
    {
        private Inventory _inventory;

        private ItemDatabase _itemDatabase;

        [Inject]
        public void Construct(Inventory inventory, ItemDatabase itemDatabase)
        {
            _inventory = inventory;
            _itemDatabase = itemDatabase;

        }


        public void Save()
        {
            InventorySaveData saveData = new InventorySaveData();
            saveData.slots = new List<SlotSaveData>();

            foreach (var slot in _inventory.Slots)
            {
                SlotSaveData slotSaveData = new SlotSaveData();

             
                if (slot.ItemData != null && slot.Count > 0)
                {
                    slotSaveData.Init(slot.ItemData.itemID, slot.Count);
                }
                else
                {
               
                    slotSaveData.Init(-1, 0); 
                }

                saveData.slots.Add(slotSaveData);
            }
            // 1. Превращаем объект saveData в JSON-строку
            string json = JsonUtility.ToJson(saveData, true); // true сделает текст красивым со сдвигами

            // 2. Создаем путь к файлу. persistentDataPath — это скрытая папка игры на компе/телефоне
            string path = Path.Combine(Application.persistentDataPath, "inventory_save.json");

            // 3. Записываем эту строчку прямо в файл
            File.WriteAllText(path, json);
    
            Debug.Log($"Игра успешно сохранена по пути: {path}");
        }
        public void Load()
        {
            // 1. Получаем тот же самый путь к файлу
            string path = Path.Combine(Application.persistentDataPath, "inventory_save.json");

            // 2. Проверяем, существует ли вообще файл (если игрок запустил игру впервые — файла нет)
            if (!File.Exists(path))
            {
                Debug.Log("Файл сохранения не найден. Начинаем новую игру.");
                return;
            }

            // 3. Читаем весь текст из файла в одну строчку
            string json = File.ReadAllText(path);

            // 4. Магия магии: JsonUtility парсит текст обратно в полноценный объект с данными
            InventorySaveData loadedData = JsonUtility.FromJson<InventorySaveData>(json);

            // 5. Теперь у тебя есть список loadedData.slots! 
            // Передаем эти данные обратно в инвентарь:
            for (int i = 0; i < loadedData.slots.Count; i++)
            {
                // На всякий случай проверяем, чтобы слотов в сохранении не было больше, чем в самом инвентаре
                if (i >= _inventory.SlotsCount) break; 

                var slotSaveData = loadedData.slots[i];
                var runtimeSlot = _inventory.GetItem(i); // Берем реальный слот инвентаря

                if (slotSaveData.itemID == -1)
                {
                    runtimeSlot.Clear(); // Если ID -1, просто очищаем слот
                }
                else
                {
               
                    ItemData itemData = _itemDatabase.GetItemById(slotSaveData.itemID);
                
                    if (itemData != null)
                    {
                        runtimeSlot.Init(itemData, slotSaveData.count);
                    }
                    else
                    {
                        Debug.LogError($"Предмет с ID {slotSaveData.itemID} не найден в ItemDatabase!");
                        runtimeSlot.Clear();
                    }
                }
            }
    
            Debug.Log("Инвентарь успешно загружен!");
        }
    }
}
