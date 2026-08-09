using _Project.Scripts.Player.Health;
using _Project.Scripts.Player.Hunger;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Inventory
{
    public class ItemUseContext
    {
        public PlayerHealth Health { get; set; }
        public PlayerHunger Hunger { get; set; }

        [Inject]
        public void Construct(PlayerHealth health, PlayerHunger hunger)
        {
            Health =  health;
            Hunger =  hunger;
        }
    }
}
