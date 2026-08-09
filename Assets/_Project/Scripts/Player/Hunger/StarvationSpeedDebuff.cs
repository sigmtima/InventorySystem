using _Project.Scripts.Player.Movement;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Hunger
{
    public class StarvationSpeedDebuff : MonoBehaviour
    {
     private PlayerController _playerController;
     private PlayerHunger _playerHunger;
     private float  _slowMultiplier;
     private float _defaultSpeed;
     
     [Inject]
     public void Construct(PlayerController playerController,  PlayerHunger playerHunger, PlayerStatsConfig playerStatsConfig)
     {
         Debug.Log("Construct");
         _playerController = playerController;
         _playerHunger = playerHunger;
         _defaultSpeed = playerController.MovementData.WalkSpeed;
         _slowMultiplier = playerStatsConfig.slowMultiplier;
         Subscribe();
     }
     
     private void  Subscribe()
     {
         _playerHunger.OnStarvationSpeedDebuffStart += StartSpeedDebuff;
         _playerHunger.OnStarvationSpeedDebuffStop +=  StopSpeedDebuff;
     }
     
     public void OnDisable()
     {
         _playerHunger.OnStarvationSpeedDebuffStart -= StartSpeedDebuff;
         _playerHunger.OnStarvationSpeedDebuffStop -=  StopSpeedDebuff;
     }
     private void StartSpeedDebuff()
     {
         Debug.Log("StartSpeedDebuff");
         _playerController.MovementData.ApplySlow(_slowMultiplier);
     }

     private void StopSpeedDebuff()
     {
         _playerController.MovementData.RemoveSlow(_defaultSpeed);
     }
    }
}
