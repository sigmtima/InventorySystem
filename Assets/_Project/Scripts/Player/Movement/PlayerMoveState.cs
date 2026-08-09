using Core;
using Player;
using UnityEngine;

namespace _Project.Scripts.Player.Movement
{
    public class PlayerMoveState : BaseState<PlayerContext>
    {
        private Vector3 _currentVelocity;
        
    
        public PlayerMoveState(PlayerContext context) : base(context)
        {
        }

        public override void Enter()
        {
            Context.Controller.SetAnimationBool("isMoving", true);
        }

        public override void Update()
        {
            if (Context.InputManager.MoveInput.x == 0f && Context.InputManager.MoveInput.y == 0f) Context.Controller.ChangeState(Context.Controller.IdleState);
        }

        public override void FixedUpdate()
        {
            Vector3 targetVelocity = new Vector3(Context.InputManager.MoveInput.x, 0f, Context.InputManager.MoveInput.y) * Context.Controller.MovementData.WalkSpeed;
            var currentSpeedDiff = Context.InputManager.MoveInput.magnitude > 0f ? Context.Controller.MovementData.Acceleration : Context.Controller.MovementData.Deceleration;
            
            Vector3 direction = new Vector3(Context.InputManager.MoveInput.x, 0f, Context.InputManager.MoveInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            Context.Controller.visualParent.rotation = Quaternion.Slerp(Context.Controller.visualParent.rotation, targetRotation, Time.deltaTime * Context.Controller.MovementData.RotationSpeed);
                
            _currentVelocity =
                Vector3.MoveTowards(_currentVelocity, targetVelocity, currentSpeedDiff * Time.fixedDeltaTime);
            
            _currentVelocity.y = Context.Rb.linearVelocity.y;
            
            Context.Rb.linearVelocity = _currentVelocity;
        }

        public override void Exit()
        {
        }
    }
}