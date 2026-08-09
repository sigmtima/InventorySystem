using Core;
using UnityEngine;

namespace _Project.Scripts.Player.Movement
{
    public class PlayerIdleState : BaseState<PlayerContext>
    {
        public PlayerIdleState(PlayerContext context) : base(context)
        {
        }

        public override void Enter()
        {
            Context.Controller.SetAnimationBool("isMoving", false);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (Context.InputManager.MoveInput.x != 0 || Context.InputManager.MoveInput.y != 0)
                Context.Controller.ChangeState(Context.Controller.MoveState);
        }

        public override void FixedUpdate()
        {
            Context.Rb.linearVelocity = new Vector3(0f, Context.Rb.linearVelocity.y, 0f);
        }
    }
}