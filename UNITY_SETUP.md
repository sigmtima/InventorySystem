# Unity Setup Guide

This guide covers the required Unity scene setup for the Inventory System.

The current package is designed around VContainer and the gameplay systems used by the original project. It is not a completely dependency-free package.

## Requirements

- Unity
- TextMeshPro
- Unity Input System
- VContainer

The current implementation also expects:

- `PlayerHealth`
- `PlayerHunger`
- `PlayerInteractController`

These systems are used by the inventory integration and `ItemUseContext`.

---

## 1. Install the Package

Import the `.unitypackage` from the repository's **Releases** section.

Alternatively, copy the inventory source files into your project.

After importing the package, install the required dependencies.

---

## 2. Create the Lifetime Scope

Create a GameObject with a `LifetimeScope`.

The inventory requires the following registrations:

```csharp
protected override void Configure(IContainerBuilder builder)
{
    builder.RegisterComponentInHierarchy<InventoryController>();
    builder.RegisterComponentInHierarchy<InventoryUI>();
    builder.RegisterComponentInHierarchy<EquipmentController>();
    builder.RegisterComponentInHierarchy<DragDropController>();

    builder.Register<Inventory>(Lifetime.Singleton)
        .WithParameter("slotsCount", inventorySlotsCount);

    builder.Register<ItemUseContext>(Lifetime.Singleton);

    builder.RegisterInstance(itemDatabase);
}
```

The important part is that `Inventory` is registered as a singleton.

All inventory-related systems will receive the same runtime `Inventory` instance through VContainer.

---

## 3. Configure Inventory Size

Set the number of inventory slots in the Lifetime Scope:

```text
Inventory Slots Count: 10
```

The number of `SlotUI` objects in `InventoryUI` must match this value.

For example:

```text
Inventory Slots Count = 10
InventoryUI Slots = 10
```

---

## 4. Create the Item Database

Create an Item Database asset:

```text
Create
└── Inventory
    └── Item Database
```

Add all available `ItemData` assets to the database.

Assign the database to the Lifetime Scope:

```text
GameLifetimeScope
└── Item Database
```

Every item should have a unique `itemID`.

---

## 5. Create Item Data

Create a base item:

```text
Create
└── Inventory
    └── ItemData
```

Configure its:

- Item ID
- Icon
- Item Type
- Maximum Stack
- Item Behavior
- Consumption settings

For specialized items, use the corresponding `ItemData` subclass.

Example:

```text
Create
└── Inventory
    └── ItemFoodData
```

`ItemFoodData` adds:

```text
Hunger Restore
```

to the base item configuration.

---

## 6. Configure Item Behaviors

Item behavior is implemented through `ItemBehavior` ScriptableObjects.

For food items, create:

```text
Create
└── Inventory
    └── Behaviors
        └── Food
```

Assign the resulting `FoodBehavior` asset to the corresponding `ItemFoodData`.

The resulting flow is:

```text
ItemFoodData
     │
     ▼
FoodBehavior
     │
     ▼
ItemUseContext
     │
     ▼
PlayerHunger
```

New behaviors can be implemented by inheriting from `ItemBehavior`.

---

## 7. Create the Inventory UI

Create an inventory UI under your Canvas:

```text
Canvas
└── InventoryUI
```

Add the `InventoryUI` component.

Create the required number of slot objects:

```text
InventoryUI
├── SlotUI
├── SlotUI
├── SlotUI
├── ...
└── SlotUI
```

Assign them to the `Slots` list of `InventoryUI`.

The order of this list defines the inventory slot indices.

---

## 8. Configure SlotUI

Each slot requires a `SlotUI` component.

A basic hierarchy can be:

```text
SlotUI
├── ItemIcon
└── ItemCount
```

Assign the required references in the inspector:

```text
SlotUI
├── Image → ItemIcon
└── Item Count → TextMeshProUGUI
```

`SlotUI` handles:

- Item icon rendering
- Stack count rendering
- Selection
- Pointer interaction
- Drag & Drop events

---

## 9. Configure Drag & Drop

Create a GameObject with:

```text
DragDropController
```

The controller requires an `Image` used as the temporary drag icon.

Example:

```text
DragDropController
└── Drag Icon
```

Assign the Image to the `Drag Icon` field.

The drag icon should start disabled.

An `EventSystem` must also exist in the scene.

For projects using the Input System, make sure the EventSystem uses the appropriate Input System UI module.

---

## 10. Configure EquipmentController

Add:

```text
EquipmentController
```

to a GameObject in the Lifetime Scope.

Assign an Image as the selection frame:

```text
EquipmentController
└── Frame → Selection Frame
```

The frame is moved to the currently selected inventory slot.

---

## 11. Configure InventoryController

Add:

```text
InventoryController
```

to a GameObject registered in the Lifetime Scope.

Its dependencies are resolved automatically through VContainer:

```text
Inventory
ItemUseContext
PlayerInteractController
EquipmentController
```

No manual references are required for these dependencies.

---

## 12. Player Integration

The current implementation connects item usage to the player's gameplay systems.

`ItemUseContext` currently provides:

```text
PlayerHealth
PlayerHunger
```

Make sure these components are available in the same VContainer scope.

`InventoryController` also listens to:

```text
PlayerInteractController.OnCollect
```

so collected items can be passed directly into the inventory.

---

## 13. Scene Example

A minimal scene can look like:

```text
Scene
│
├── GameLifetimeScope
│
├── Player
│   ├── PlayerHealth
│   ├── PlayerHunger
│   └── PlayerInteractController
│
├── InventorySystem
│   ├── InventoryController
│   ├── DragDropController
│   └── EquipmentController
│
├── Canvas
│   └── InventoryUI
│       ├── SlotUI
│       ├── SlotUI
│       └── ...
│
└── EventSystem
```

## 14. Creating a Custom Item and Behavior

To create a custom item with its own behavior, follow these steps.

### 1. Create a Custom Item Data Type

Create a new class derived from `ItemData`.

For example, a stamina-restoring item could use:

```csharp
using UnityEngine;

[CreateAssetMenu(
    fileName = "ItemStaminaData",
    menuName = "Inventory/ItemStaminaData"
)]
public class ItemStaminaData : ItemData
{
    public int staminaRestore;
}
```

The new item type will then be available in Unity under:

```text
Create
└── Inventory
    └── ItemStaminaData
```

Create the asset and configure its item-specific data, such as the amount of stamina restored.

### 2. Create a Custom Behavior

Create a new class derived from `ItemBehavior` and implement the `Use` method.

For example:

```csharp
using UnityEngine;

[CreateAssetMenu(
    fileName = "StaminaBehavior",
    menuName = "Inventory/Behaviors/Stamina"
)]
public class StaminaBehavior : ItemBehavior
{
    public override void Use(ItemUseContext context, ItemData data)
    {
        if (data is not ItemStaminaData staminaData)
            return;

        context.PlayerStamina.Restore(staminaData.staminaRestore);
    }
}
```

The behavior will then be available in Unity under:

```text
Create
└── Inventory
    └── Behaviors
        └── Stamina
```

Create the behavior asset.

### 3. Create the Item

Create your custom item:

```text
Create
└── Inventory
    └── ItemStaminaData
```

Configure its properties:

```text
Item ID:          10
Icon:             Stamina Potion
Max Stack:        5
Stamina Restore:  25
```

Then assign the `StaminaBehavior` asset to the item's `Item Behavior` field.

The final configuration is:

```text
Stamina Potion
├── ItemStaminaData
│   ├── Item ID: 10
│   ├── Max Stack: 5
│   └── Stamina Restore: 25
│
└── Item Behavior
    └── StaminaBehavior
```

### 4. Add the Item to the Item Database

Open your `ItemDatabase` and add the newly created item to the list of available items.

Make sure its `Item ID` is unique.

The final runtime flow is:

```text
Stamina Potion
      │
      ▼
StaminaBehavior
      │
      ▼
ItemUseContext
      │
      ▼
PlayerStamina.Restore()
```

This approach keeps item configuration inside `ItemData` and gameplay logic inside `ItemBehavior`.

You can use the same pattern for other item types, such as:

- ammunition
- temporary buffs
- mana restoration
- teleport items
- experience items
- quest items
- items that trigger custom gameplay events

Each behavior can define its own logic without modifying the core `Inventory` implementation.


The exact hierarchy is not required. The important part is that the required components are registered in the appropriate VContainer scope.

---

## 15. Setup Checklist

- [ ] VContainer is installed.
- [ ] TextMeshPro is available.
- [ ] Unity Input System is configured.
- [ ] A `LifetimeScope` exists.
- [ ] `Inventory` is registered.
- [ ] `InventoryUI` is registered.
- [ ] `InventoryController` is registered.
- [ ] `DragDropController` is registered.
- [ ] `EquipmentController` is registered.
- [ ] `ItemDatabase` is assigned.
- [ ] The inventory slot count matches the number of `SlotUI` objects.
- [ ] Every `SlotUI` has its icon and count references assigned.
- [ ] `DragDropController` has a drag icon assigned.
- [ ] `EquipmentController` has a selection frame assigned.
- [ ] An `EventSystem` exists.
- [ ] `PlayerHealth` and `PlayerHunger` are available to `ItemUseContext`.
- [ ] `PlayerInteractController` is available to `InventoryController`.
- [ ] Item IDs are unique.
- [ ] Items are added to `ItemDatabase`.

## Notes

The `SaveLoad` implementation is currently present in the codebase but has not been fully tested or integrated into the documented scene setup. It is intentionally excluded from the required setup flow until it has been verified.