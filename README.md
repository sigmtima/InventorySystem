# Inventory System

A modular inventory system developed in Unity with a focus on clean architecture, separation of responsibilities, and data-driven design.

📸 Screenshots
Screenshots will be added as the project progresses.

🎥 GIFs / Demonstrations
Gameplay demonstrations and system GIFs will be added here.

✨ Current Features
- Item pickup through player interaction
- Inventory data management & customizable slots
- Item stacking with configurable maximum stack size
- ScriptableObject-based item data
- Item behaviors decoupled from inventory logic
- `ItemUseContext` for providing gameplay dependencies to items
- Event-driven communication between inventory and UI
- Visual item icons and stack count rendering

🏗️ Architecture
The inventory is structured into decoupled layers:

Player
  ↓
PlayerInteractController
  ↓
ICollectible
  ↓
InventoryController
  ↓
Inventory
  ↓
InventorySlot
  ↓
Inventory UI

- **Inventory:** Stores inventory slots and manages adding, removing, and swapping items.
- **InventorySlot:** Represents a stack of a specific item, tracking current amount and item data.
- **ItemData:** ScriptableObject containing static metadata (ID, type, icon, max stack, behavior reference).
- **ItemBehavior:** Defines item-specific usage logic without polluting core inventory classes.
- **InventoryController:** Main entry point for inventory gameplay operations (picking up, using items).
- **InventoryUI:** Handles UI rendering driven by inventory events.

💡 Why This Architecture?
- **Lightweight DI:** I chose **VContainer** over heavier frameworks like Zenject because it provides high performance, minimal boilerplate, and fits perfectly into modern Unity architectures.
- **Pure Data vs. Behavior:** Separating `ItemBehavior` from `ItemData` keeps ScriptableObjects as clean data containers. This avoids monolithic scripts, eliminates spaghetti dependencies, and lets me add new item types without modifying existing inventory code.

🧠 Challenges & Lessons Learned
- **The Problem (Context & Dependencies):** A major challenge during architecture design was figuring out how isolated items should execute their behavior. Items needed access to external gameplay systems (e.g., player stats for healing, sound managers, or world effects), but hardcoding scene references or relying on global Singletons would destroy modularity and testability.
- **The Solution (`ItemUseContext`):** I designed an `ItemUseContext` structure. When an item is used, the caller creates a contextual payload containing only the required dependencies and passes it down to the `ItemBehavior`. This keeps items completely decoupled from scene architecture while giving them safe access to gameplay systems.

📦 Item Stacking
Items can be added in arbitrary quantities.
For example, if an item has a maximum stack size of 10 and the player receives 27 items:

[10] [10] [7]

Existing compatible stacks are filled before creating new ones.

🛠️ Tech Stack
- **Engine:** Unity
- **Language:** C#
- **Architecture:** Event-driven, Data-driven (ScriptableObjects), Dependency Injection
- **Packages:** Unity Input System, VContainer

🎯 Planned Features
- Item selection & hotbar usage
- Item usage through UI
- Drag & Drop support
- Equipment system
- Inventory updates and synchronization
- Save / Load system
- Crafting system

📊 Project Status
🚧 **In Development**
Developed as a portfolio project focused on architecture scalability, C# design patterns, modularity, and clean code principles.