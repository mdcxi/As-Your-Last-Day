using As_Your_Last_Day.UI;
using UnityEngine;

namespace As_Your_Last_Day
{
    public class EnemyAttack : MonoBehaviour
    {
        private PlayerHealth _target;
        [SerializeField] private float damage = 40f;

        void Start()
        {
            _target = FindObjectOfType<PlayerHealth>();
        }

        public void AttackHitEvent ()
        {
            //if we don't have a target
            if (_target == null) return;
            _target.TakeDamage(damage);
            _target.GetComponent<DisplayDamage>().ShowDamageImpact();
        }
    }
}

