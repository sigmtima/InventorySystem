using Player;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace _Project.Scripts.Player.Movement
{
    public class RuntimeMovementData
    {
        public float WalkSpeed;
        public  float Acceleration;
        public  float Deceleration;
        public  float RotationSpeed;

        [Inject]
        public void Construct(MovementData movementData)
        {
            Debug.Log("Construct у  даты ");
            WalkSpeed = movementData.walkSpeed;
            Acceleration = movementData.acceleration;
            Deceleration = movementData.deceleration;
            RotationSpeed = movementData.rotationSpeed;
        }
        
        public void ApplySlow(float multiplier)
        {
            WalkSpeed *= multiplier;
        }

        public void RemoveSlow(float defaultSpeed)
        {
            WalkSpeed = defaultSpeed;
        }
    }
}
