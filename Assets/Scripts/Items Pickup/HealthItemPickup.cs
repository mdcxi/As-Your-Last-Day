using Lean.Pool;
using UnityEngine;

namespace As_Your_Last_Day.Pickups
{
    public class HealthItemPickup : MonoBehaviour
    {
        [SerializeField] private float healthValue = 10f;
        private PlayerHealth _playerHealth;
        private AudioSource _audio;

        private void Awake() 
        {
            _audio = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _audio.Play();
                _playerHealth.IncreaseHealth(healthValue);
                // Destroy(gameObject,.2f);
                LeanPool.Despawn(gameObject, .2f);
            }
        }
    }
}

