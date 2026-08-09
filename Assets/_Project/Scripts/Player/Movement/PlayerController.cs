using Core;
using Input;
using Player;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Movement
{
    public class PlayerController : StateMachine<PlayerContext>
    {
        [SerializeField] private InputManager inputManager;
        
         private RuntimeMovementData _movementData;
        
        [Header("Visuals & Animation Sync")] [SerializeField] private Animator bodyAnimator;

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        
        public PlayerIdleState IdleState => _idleState; 
        public PlayerMoveState MoveState => _moveState;
        public RuntimeMovementData  MovementData => _movementData;
        
        public Transform visualParent;
        [SerializeField] private float animationSpeed = 1;
        
        private PlayerContext _context;

        private Rigidbody _playerRigidbody;

        [Inject]
        public void Construct(RuntimeMovementData movementData)
        {
            _movementData = movementData;
        }
        
        private void Awake()
        {
            
            _playerRigidbody = GetComponent<Rigidbody>();
            _context = new PlayerContext(this, _playerRigidbody, bodyAnimator,inputManager);
            
            _idleState= new PlayerIdleState(_context);
            _moveState= new PlayerMoveState(_context);
            
            DontDestroyOnLoad(gameObject);

            if (_playerRigidbody == null) Debug.LogError("Rigidbody не найден на объекте!");
        
            if (visualParent == null) visualParent = transform;
        }

        private void Start()
        {
            ChangeState(_idleState);
            bodyAnimator.speed = animationSpeed;
        }
    
        public void SetAnimationBool(string name, bool value)
        {
            bodyAnimator.SetBool(name, value);
        }

        private void RotateTowardsMouse()
        {
            
        }
    }
}