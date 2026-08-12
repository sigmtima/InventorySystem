using _Project.Scripts.Inventory;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Health;
using _Project.Scripts.Player.Hunger;
using _Project.Scripts.Player.Movement;
using Input;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Core
{
    public class GameLifetimeScope: LifetimeScope
    {
        [SerializeField] private PlayerStatsConfig playerStatsConfig;
        [SerializeField] private MovementData movementData;
        [SerializeField] private PlayerHunger playerHunger;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DamageFlashUI>();
            builder.RegisterComponentInHierarchy<PlayerInteractController>();
            builder.RegisterComponentInHierarchy<EquipmentController>();
            builder.RegisterComponentInHierarchy<InputManager>();
            builder.RegisterComponentInHierarchy<InventoryController>();
            builder.RegisterComponentInHierarchy<InventoryUI>();
            builder.Register<Inventory.Inventory>(Lifetime.Singleton);
            builder.Register<ItemUseContext>(Lifetime.Singleton);
            builder.Register<RuntimeMovementData>(Lifetime.Singleton);
            builder.RegisterInstance(playerStatsConfig);
            builder.RegisterInstance(movementData);
            builder.RegisterComponentInHierarchy<PlayerController>();
            builder.RegisterComponentInHierarchy<PlayerHunger>();
            builder.RegisterComponentInHierarchy<PlayerHealth>();
            builder.RegisterComponentInHierarchy<PlayerStarvationSystem>();
            builder.RegisterComponentInHierarchy<HungerUI>();
            builder.RegisterComponentInHierarchy<PlayerHealthUI>();
            builder.RegisterComponentInHierarchy<StarvationSpeedDebuff>();
        }
    }
}