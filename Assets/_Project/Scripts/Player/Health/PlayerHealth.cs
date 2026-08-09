using _Project.Scripts.Core;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Health
{
    public class PlayerHealth : MonoBehaviour, IHealable, IDamageable
    {
         private int _maxHealth;
          public int CurrentHealth { get; private set; }

        public event System.Action<int> OnHealthChanged;
        
        public event System.Action OnTakeDamage;
        public event System.Action OnHeal;
        
        private PlayerStatsConfig _config;

        [Inject]
        public void Construct(PlayerStatsConfig config)
        {
            _config = config;
            _maxHealth = _config.startHealth;
            CurrentHealth = _maxHealth;
        }
        
        
        
        public void TakeDamage(int amount)
        {   
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            OnTakeDamage?.Invoke();
            OnHealthChanged?.Invoke(CurrentHealth);
            Debug.Log(CurrentHealth);
            
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        private void Die()
        {
            Debug.Log("Die!!!");
            Destroy(gameObject);
        }
    }
}
