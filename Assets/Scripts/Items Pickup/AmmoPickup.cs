using Lean.Pool;
using UnityEngine;

namespace As_Your_Last_Day.Pickups
{
    public class AmmoPickup : MonoBehaviour
    {
        [SerializeField] private int ammoAmount = 5;
        [SerializeField] private AmmoType ammoType;

        private AudioSource _pickupAudio;

        private void Start() 
        {
            _pickupAudio = GetComponent<AudioSource>();
        }
        
        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _pickupAudio.Play();
                FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
                
                /*
                 * This line immediately destroys the GameObject although I set destroyTime is 3 seconds
                 * I can fix that by creating a code like this: _pickup = LeanPool.Spawn (prefab, position is some where, e.g. at the position when an enemy dies)
                 * The code below will work as expected
                 * But I don't have much time to make everything go well, I have another big project, so yeah just keep this code in comment, and you can fix that if you want!
                 * 
                */
                // LeanPool.Despawn(_pickup, 3f); 
                Destroy(gameObject, .2f); //I decided to replace by this line though I know It doesn't good for performance much 
            }
        }
    }
}

