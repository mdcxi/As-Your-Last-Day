using Lean.Pool;
using UnityEngine;

namespace As_Your_Last_Day.Pickups
{
    public class BatteryPickup : MonoBehaviour
    {
        [SerializeField] private float restoreAngle = 90f; 
        [SerializeField] private float addIntensity = 1f;

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
                other.GetComponentInChildren<FlashlightSystem>().RestoreLightAngle(restoreAngle);
                other.GetComponentInChildren<FlashlightSystem>().AddLightIntensity(addIntensity);
                // Destroy(gameObject, .2f);
                LeanPool.Despawn(gameObject, .2f);
            }
        }
    }
}

