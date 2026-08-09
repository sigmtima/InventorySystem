using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using _Project.Scripts.Player.Health;
using VContainer;

namespace _Project.Scripts.Player.Health
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;

        private PlayerHealth _playerHealth;

        [Inject]
        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.OnHealthChanged += ChangeHealthUI;
            healthText.text = _playerHealth.CurrentHealth.ToString();
        }
        
        private void ChangeHealthUI(int amount)
        {
            healthText.text = amount.ToString();
        }

        public void OnDisable()
        {
            _playerHealth.OnHealthChanged -= ChangeHealthUI;
        }
    }
}
