using Lean.Pool;
using UnityEngine;

namespace As_Your_Last_Day.Pickups
{
    public class AmmoPickup : MonoBehaviour
    {
        [SerializeField] private int ammoAmount = 5;
        [SerializeField] private AmmoType ammoType;

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
                FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
                // Destroy(gameObject, .2f);
                LeanPool.Despawn(gameObject, .2f);
            }
        }
    }
}

