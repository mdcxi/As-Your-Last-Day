using UnityEngine;
using UnityEngine.AI;

namespace As_Your_Last_Day
{
    public class EnemyAI : MonoBehaviour
    { 
        [SerializeField] private float chaseRange = 5f;
        [SerializeField] private float turnSpeed = 5f;
        
        private NavMeshAgent _navMeshAgent;
        private Transform _target;
        private float _distanceToTarget = Mathf.Infinity;
        private bool _isProvoked = false;
        
        private EnemyHealth _enemyHealth;
        
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _target = FindObjectOfType<PlayerHealth>().transform;
        }

        void Update()
        {
            if (_enemyHealth.IsDead())
            {
                enabled = false;
                _navMeshAgent.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                return;
            }

            _distanceToTarget = Vector3.Distance(_target.position, transform.position);

            if (_isProvoked)
            {
                EngageTarget();
            }
            else if (_distanceToTarget <= chaseRange)
            {
                _isProvoked = true;
            }  
        }

        public void OnDamageTaken ()
        {
            _isProvoked = true;
        }
        private void EngageTarget()
        {
            FaceTarget();
            if (_distanceToTarget >= _navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }

            if (_distanceToTarget <= _navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
        }

        private void ChaseTarget()
        {
            GetComponent<Animator>().SetBool("attack", false);
            GetComponent<Animator>().SetTrigger("move");      //when we chase target, set the trigger "move" in our animator 
            _navMeshAgent.SetDestination(_target.position);          //the enemy will chase the player   
        }

        private void AttackTarget()
        {
            GetComponent<Animator>().SetBool("attack", true);
        }

        private void FaceTarget ()
        {
            Vector3 direction =  (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            //transform.rotation = where the target is, we need to rotate at a certain speed 
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere (transform.position, chaseRange);
        }

        public void IncreaseChaseRange ()
        {
            chaseRange += 10;
        }
    }
}

