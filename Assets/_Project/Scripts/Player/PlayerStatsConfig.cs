using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "Scriptable Objects/Player/PlayerStatsConfig")]
    public class PlayerStatsConfig : ScriptableObject
    {
        [Header("Health")]
        
        public int startHealth;
        
        [Header("Hunger")]
        
        public int startHunger;
        public int foodLossAmount;
        public int slowdownHungerThreshold;
        public int starvationTickInterval;
        public int foodLossInterval;
        public int healthLossPerStarvationTick;
        public float slowMultiplier;
        
    }
}
