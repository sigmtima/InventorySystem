using System;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Health
{
    public class DamageFlashUI : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        [SerializeField]  private  Animator damageAnimator;
        [SerializeField]  private  Animator healAnimator;
        [SerializeField] private string damageAnimationName;
        [SerializeField] private string healAnimationName;
        
        [Inject]
        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.OnTakeDamage += DamageFlash;
            _playerHealth.OnHeal += HealFlash;
        }

        public void OnDisable()
        {
            _playerHealth.OnTakeDamage -= DamageFlash;
            _playerHealth.OnHeal -= HealFlash;
        }

        private void DamageFlash()
        {
            damageAnimator.SetTrigger(damageAnimationName);
        }

        private void HealFlash()
        {
            healAnimator.SetTrigger(healAnimationName);
        }
    }
}
