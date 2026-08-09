using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Player.Health;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Hunger
{
    public class PlayerStarvationSystem : MonoBehaviour
    {
        private PlayerHunger _playerHunger;
        private PlayerHealth _playerHealth;
        private PlayerStatsConfig _playerStatsConfig;
        
        private int _starvationTickInterval;
        private int _healthLossPerStarvationTick;
        
        private Coroutine _starvationRoutine;
        
        private bool _isSubscribed;

          public void OnDisable()
        {
            Unsubscribe();
        }

        private void  Subscribe()
        {
            if (_isSubscribed) return;
            _playerHunger.OnDeteriorateFromHungerStart += StartDeteriorateFromHunger;
            _playerHunger.OnDeteriorateFromHungerStop += StopDeteriorateFromHunger;
            _isSubscribed = true;
        }
        private void Unsubscribe()
        {
            if (!_isSubscribed) return;

            _playerHunger.OnDeteriorateFromHungerStart -= StartDeteriorateFromHunger;
            _playerHunger.OnDeteriorateFromHungerStop -= StopDeteriorateFromHunger;
            _isSubscribed = false;
        }
    
        [Inject]
        public void Construct(PlayerHunger playerHunger, PlayerHealth playerHealth, PlayerStatsConfig playerStatsConfig)
        {
            Debug.Log("Constructing PlayerStarvationSystem"); 
            _playerHunger = playerHunger;
            _playerHealth = playerHealth;
            _playerStatsConfig = playerStatsConfig;
            Subscribe();
        }

        public void Start()
        {
          _starvationTickInterval = _playerStatsConfig.starvationTickInterval;;
         _healthLossPerStarvationTick= _playerStatsConfig.healthLossPerStarvationTick ;
        }

        private void StartDeteriorateFromHunger()
        {
            if (_starvationRoutine != null)
                return;

            _starvationRoutine = StartCoroutine(DeteriorateFromHunger());
        }

        private void StopDeteriorateFromHunger()
        {
            if (_starvationRoutine == null)
                return;

            StopCoroutine(_starvationRoutine);
            _starvationRoutine = null;
        }
       

        private IEnumerator DeteriorateFromHunger()
        {
            while (true)
            {
                yield return new WaitForSeconds(_starvationTickInterval);
               _playerHealth.TakeDamage(_healthLossPerStarvationTick);
            }
        }
    }
}
