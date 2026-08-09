using _Project.Scripts.Player;
using _Project.Scripts.Player.Movement;
using Input;
using JetBrains.Annotations;
using Player;
using UnityEngine;

public class PlayerContext
{
    public readonly PlayerController Controller;
    public readonly Animator PlayerAnimator;
    public readonly Rigidbody Rb;
    public InputManager InputManager;

    public PlayerContext(PlayerController controller, Rigidbody rb, [CanBeNull] Animator playerAnimator, InputManager inputManager)
    {
        Controller = controller;
        Rb = rb;
        PlayerAnimator = playerAnimator;
        InputManager = inputManager;
    }
}