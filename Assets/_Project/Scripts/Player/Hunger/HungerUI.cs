using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using _Project.Scripts.Player.Health;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player.Hunger
{
    public class HungerUI : MonoBehaviour
    {
    
        [SerializeField] private TextMeshProUGUI hungerText;
        
        private PlayerHunger _playerHunger;
        
        private bool _isSubscribed;

        public void Start()
        {
            Subscribe();
        }
         
        [Inject]
        public void Construct(PlayerHunger playerHunger)
        {
            _playerHunger = playerHunger;
        }

        private void ChangeHealthUI(int amount)
        {
            hungerText.text = amount.ToString();
        }

        public void OnDisable()
        {
            Unsubscribe();
        }

        private void  Subscribe()
        {
            if (_isSubscribed) return;
            _playerHunger.OnHungerChanged += ChangeHealthUI;
            ChangeHealthUI(_playerHunger.CurrentHunger);
            _isSubscribed = true;
        }
        private void Unsubscribe()
        {
            if (!_isSubscribed) return;

            _playerHunger.OnHungerChanged -= ChangeHealthUI;
            _isSubscribed = false;
        }
    }
}
