using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "Player")]
    public class MovementData : ScriptableObject
    {
        [FormerlySerializedAs("WalkSpeed")] [Header("Walking Settings")] public float walkSpeed = 4f;

        public float acceleration = 10f;
        public float deceleration = 12f;

        [Header("Physics")] public float rotationSpeed = 15f;

        public float massValue = 1f;
    }
}