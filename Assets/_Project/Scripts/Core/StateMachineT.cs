using UnityEngine;

namespace Core
{
    public abstract class StateMachine<T> : MonoBehaviour
    {
        protected BaseState<T> CurrentState;

    
        protected virtual void Update()
        {
            CurrentState?.Update();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public void ChangeState(BaseState<T> newState)
        {
            if (CurrentState != null) CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}