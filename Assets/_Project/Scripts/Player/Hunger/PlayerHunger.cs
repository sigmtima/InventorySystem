using System.Collections;

using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Hunger
{
    public class PlayerHunger : MonoBehaviour
    {
        private int _maxHunger;
        public int CurrentHunger { get; private set; }
        private int _foodLossAmount;
        private int _slowdownHungerThreshold;
        private PlayerStatsConfig _config;
        private int _foodLossInterval;

        private bool _isDeteriorateFromHunger;
        private bool _isStarvationSpeedDebuff = false;
        
        public event System.Action<int> OnHungerChanged;

        public event System.Action OnDeteriorateFromHungerStart;
        public event System.Action OnDeteriorateFromHungerStop;
        
        public event System.Action OnStarvationSpeedDebuffStart;
        public event System.Action OnStarvationSpeedDebuffStop;

        [Inject]
        public void Construct(PlayerStatsConfig config)
        {
            _config = config;
            _foodLossAmount = _config.foodLossAmount;
            _maxHunger = _config.startHunger;
            CurrentHunger = _maxHunger;
            _slowdownHungerThreshold = _config.slowdownHungerThreshold;
            _foodLossInterval =  _config.foodLossInterval; 
            StartCoroutine(StarvingRoutine());
        }
        
        public void TakeHungry(int amount)
        {
            CurrentHunger -= amount;
            CurrentHunger = Mathf.Clamp(CurrentHunger, 0, _maxHunger);
            OnHungerChanged?.Invoke(CurrentHunger);
            Debug.Log("Hunger: " + CurrentHunger);
            
            if (CurrentHunger <= 0 && !_isDeteriorateFromHunger)
            {
                OnDeteriorateFromHungerStart?.Invoke();
                _isDeteriorateFromHunger = true;
                StopCoroutine(StarvingRoutine());
             
            }

            if (CurrentHunger > 0 && _isDeteriorateFromHunger)
            {
                OnDeteriorateFromHungerStop?.Invoke();
                _isDeteriorateFromHunger = false;
              
                StartCoroutine(StarvingRoutine());
            }

            if (CurrentHunger <= _slowdownHungerThreshold&& !_isStarvationSpeedDebuff)
            {
                _isStarvationSpeedDebuff = true;
                OnStarvationSpeedDebuffStart?.Invoke();
            }
            if (CurrentHunger > _slowdownHungerThreshold && _isStarvationSpeedDebuff)
            {
                _isStarvationSpeedDebuff = false;
                OnStarvationSpeedDebuffStop?.Invoke();
            }
        }

        public void AddHunger(int amount)
        {
            CurrentHunger += amount;
            CurrentHunger = Mathf.Clamp(CurrentHunger, 0, _maxHunger);
            OnHungerChanged?.Invoke(CurrentHunger);
        }
        
        public IEnumerator StarvingRoutine()
        {  
            while (true) 
            {
                yield return new WaitForSeconds(_foodLossInterval);
                
                TakeHungry(_foodLossAmount);
            }
        }

      
    }
}
