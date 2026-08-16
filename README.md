# Unity Inventory System

A modular, data-driven inventory system for Unity built with C#, ScriptableObjects, events, and VContainer.

Designed as a reusable gameplay system with clear separation between runtime state, item definitions, UI, and persistence.

## Features

- **Slot-based inventory** — configurable slot count, stacking, item removal, and slot swapping.
- **ScriptableObject items** — reusable item definitions with IDs, icons, stack limits, and usage configuration.
- **Polymorphic item behavior** — extend `ItemBehavior` to add new item types without modifying the inventory core.
- **Event-driven UI** — inventory and slot state changes are propagated through events.
- **Drag & Drop** — dedicated controller for moving items between slots.
- **Item selection & usage** — UI-driven item selection with behavior-based execution.
- **JSON persistence** — save/load inventory state using stable item IDs instead of Unity object references.
- **Item database** — centralized ID → `ItemData` resolution for persistence.
- **Dependency injection** — VContainer manages runtime dependencies and keeps the inventory model independent from `MonoBehaviour`.

## Architecture

```text
                         ItemDatabase
                              │
                              ▼
                          ItemData
                              │
                    ┌─────────┴─────────┐
                    ▼                   ▼
              ItemBehavior        ItemFoodData
                    │
                    ▼
               Inventory
                    │
                    ▼
             InventorySlot
                    │
          ┌─────────┴─────────┐
          ▼                   ▼
    InventoryUI          SaveLoadManager
          │
          ▼
        SlotUI
          │
          ▼
 DragDropController
```

### Core responsibilities

| Component | Responsibility |
|---|---|
| `Inventory` | Runtime inventory state, stacking, removal, swapping |
| `InventorySlot` | Item reference, stack count, slot-level change events |
| `ItemData` | Static item configuration |
| `ItemBehavior` | Polymorphic item usage logic |
| `ItemDatabase` | Item ID → `ItemData` lookup |
| `InventoryUI` | Synchronizes inventory state with slot UI |
| `SlotUI` | Item rendering, selection, pointer/drag events |
| `DragDropController` | Drag visualization and slot swapping |
| `SaveLoadManager` | JSON serialization and restoration |

The runtime model is kept separate from Unity UI objects. Item definitions are stored as ScriptableObjects, while save data contains only serializable runtime state.

## Item System

Items are defined through ScriptableObjects. Specialized data can extend `ItemData` without changing the inventory implementation.

```csharp
[CreateAssetMenu(fileName = "ItemFoodData", menuName = "Inventory/ItemFoodData")]
public class ItemFoodData : ItemData
{
    public int hungerRestore;
}
```

Usage behavior is separated from item data through `ItemBehavior`:

```csharp
public abstract class ItemBehavior : ScriptableObject
{
    public abstract void Use(ItemUseContext context, ItemData data);
}
```

This allows new behaviors to be added independently from the inventory core.

## Save Format

Inventory state is serialized as item IDs and stack counts:

```json
{
    "slots": [
        {
            "itemID": 1,
            "count": 5
        },
        {
            "itemID": -1,
            "count": 0
        }
    ]
}
```

`ItemDatabase` resolves the saved IDs back to `ItemData` during loading.

This avoids serializing Unity object references such as `ScriptableObject` assets or sprites.

## Project Structure

```text
Inventory/
│
├── Inventory.cs
├── InventorySlot.cs
├── InventoryController.cs
│
├── UI/
│   ├── InventoryUI.cs
│   └── SlotUI.cs
│
├── Items/
│   ├── ItemData.cs
│   ├── ItemFoodData.cs
│   ├── ItemBehavior.cs
│   └── FoodBehavior.cs
│
├── SaveLoad/
│   ├── ItemDatabase.cs
│   ├── SaveLoadManager.cs
│   └── InventorySaveData.cs
│
├── DragDropController.cs
├── EquipmentController.cs
└── ItemUseContext.cs
```

## Dependencies

- Unity
- TextMeshPro
- Unity Input System
- [VContainer](https://vcontainer.hadashikick.jp/)

The current implementation integrates with project-specific player systems such as `PlayerHealth`, `PlayerHunger`, and `PlayerInteractController`.

## Installation

### Unity Package

Ready-to-import `.unitypackage` builds are available in **[Releases](../../releases)**.

### Source

```bash
git clone <repository-url>
```

Then import the `Inventory` scripts into your Unity project and configure the required dependencies.

See [`UNITY_SETUP.md`](UNITY_SETUP.md) for scene and dependency configuration.

## Design Goals

- Keep runtime inventory logic independent from UI.
- Use ScriptableObjects for static item configuration.
- Use polymorphism instead of type-specific logic in the inventory core.
- Communicate state changes through events.
- Keep persistence data independent from Unity object references.
- Use dependency injection instead of global state where practical.

## Current Scope

The system is currently designed around the inventory requirements of the project it was developed for. Player-specific integrations can be replaced when adapting it to another project.

The repository is focused on the inventory codebase and its reusable Unity package rather than a complete game project.

## License

See the repository license for usage and redistribution terms.