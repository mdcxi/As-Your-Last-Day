using As_Your_Last_Day.UI;
using UnityEngine;
using UnityEngine.UI;

namespace As_Your_Last_Day
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        [SerializeField] private AudioSource zombieHurtAudio;
        [SerializeField] private VFXHandler bloodVFX;
        [SerializeField] private EnemyAI chaseRange;
        [SerializeField] private GameObject enemyBorder;
        [SerializeField] private Light enemyLight;
        
        public Slider healthBar;
        
        private bool _isDead = false;
        private AudioSource _zombieDiedAudio;
        private EnemyDisplayScore _enemyCanvasDisplayScore;
        
        public bool IsDead() 
        {  
            return _isDead; 
        }
    
        void Awake()
        {
            _zombieDiedAudio = GetComponent<AudioSource>();
            _enemyCanvasDisplayScore = FindObjectOfType<EnemyDisplayScore>();
        }
    
        private void Start() 
        {
            healthBar.value = healthPoints;
        }
    
        public void TakeDamage(float damage)
        {
            chaseRange.IncreaseChaseRange();
            healthPoints -= damage;
            healthBar.value = healthPoints; 
            zombieHurtAudio.Play();
            bloodVFX.PlayBloodVFX();
    
            if (healthPoints <= 0 && !_isDead)
            {
                Die();       
                healthBar.gameObject.SetActive(false);
                enemyBorder.gameObject.SetActive(false);
                enemyLight.enabled = false;
            }
        }
    
        private void Die()
        {
            if (_isDead) return;
            _isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            zombieHurtAudio.Stop();
            _zombieDiedAudio.Play();
            _enemyCanvasDisplayScore.DisplayZombieDiedCanvas();
        }
    }
}

