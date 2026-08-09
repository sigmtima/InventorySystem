namespace Core
{
    public abstract class BaseState<T>
    {
        protected readonly T Context;

        public BaseState(T context)
        {
            Context = context;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void Exit();
    }
}