using As_Your_Last_Day.UI;
using UnityEngine;
using UnityEngine.UI;

namespace As_Your_Last_Day
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private float health = 100f;
 
        private void Start() 
        {
            healthBar.value = health;
        }
        public void TakeDamage (float damage)
        {
            health -= damage;
            healthBar.value = health;

            if (health <= 0)
            {
                GetComponent<DeathHandler>().HandleDeath();
            }
        }

        public void IncreaseHealth (float healthValue)
        {
        
            health += healthValue;
            healthBar.value = health;

            if (health > 100)
            {
                health += 0;
            }
        }
    }
}

