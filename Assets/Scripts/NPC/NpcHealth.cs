using System.Collections;
using As_Your_Last_Day.UI;
using UnityEngine;
using UnityEngine.UI;

namespace As_Your_Last_Day
{
    public class NpcHealth : MonoBehaviour
    {
        [SerializeField] private float npcHealth = 30f;
        [SerializeField] private AudioSource npcHurtAudio;
        [SerializeField] private GameObject npcBorder;
        [SerializeField] private Light npcLight;
        [SerializeField] private VFXHandler bloodVfx;
    
        public Slider healthBar;
        private bool _isDead;
        private AudioSource _npcDiedAudio;
        
        public bool IsDead() 
        {  
            return _isDead; 
        }
    
        private void Awake()
        {
            _npcDiedAudio = GetComponent<AudioSource>();
        }
    
        private void Start() 
        {
            healthBar.value = npcHealth;
        }
    
        public void TakeDamage (float damage)
        {
            npcHealth -= damage;
            healthBar.value = npcHealth; 
            Hurt();
            bloodVfx.PlayBloodVFX();
            
            if (npcHealth <= 0 && !_isDead)
            {
                Die();  
                DisableNpcHUD();
            }
        }

        private void DisableNpcHUD()
        {
            npcBorder.SetActive(false);
            npcLight.enabled = false;     
            healthBar.gameObject.SetActive(false);
        }
    
        private void Die()
        {
            if (_isDead) return;
            _isDead = true;
    
            GetComponent<Animator>().SetBool("hurt", false);
            GetComponent<Animator>().SetBool("die", true);
    
            npcHurtAudio.Stop();
            _npcDiedAudio.Play();
    
            StartCoroutine(DisplayPanelAfterDeath());
        }
    
        private void Hurt ()
        {
            GetComponent<Animator>().SetBool("hurt", true);
            npcHurtAudio.Play();
        }
    
        IEnumerator DisplayPanelAfterDeath ()
        {
            yield return new WaitForSeconds(1);
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}

