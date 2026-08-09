using _Project.Scripts.Player.Health;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        

        [SerializeField] private AudioSource audioSource; 
        [SerializeField] private AudioClip damageSound;
        
        [Inject]
        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.OnTakeDamage += PlayOneShotDamageSound;
        }

        private void PlayOneShotDamageSound()
        {
            audioSource.PlayOneShot(damageSound);
        }
        
        
    }
}
