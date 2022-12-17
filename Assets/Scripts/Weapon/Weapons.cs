using System.Collections;
using Lean.Pool;
using UnityEngine;
using TMPro;

namespace As_Your_Last_Day.Weapon
{
    public class Weapons : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float range = 100f;
        [SerializeField] private float damage = 30f;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private Ammo ammoSlot;
        [SerializeField] private AmmoType ammoType;
        [SerializeField] private float timeBetweenShots = 0.5f;
        [SerializeField] private TextMeshProUGUI ammoText;

        private AudioSource _weaponAudio;

        private bool _canShoot = true;

        private void OnEnable() 
        {
            _canShoot = true;
        }

        void Awake()
        {
            _weaponAudio = GetComponent<AudioSource>();
        }

        void Update()
        {
            DisplayAmmo();

            if (Input.GetMouseButtonDown(0) && _canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
        
        private void DisplayAmmo ()
        {
            int currentAmmo = ammoSlot.GetCurrentAmmo (ammoType);
            ammoText.text = currentAmmo.ToString();
        }

        private IEnumerator Shoot()
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) >0)
            {
                PlayMuzzleFlash();
                ProcessRaycast();
                ProcessRaycastNPC();
                ammoSlot.ReduceCurrentAmmo(ammoType);

                _weaponAudio.Play();
            } 
            else 
            {
                _weaponAudio.Stop();
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }

        public void PlayMuzzleFlash()
        {
            muzzleFlash.Play();
        }

        private void ProcessRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                target.TakeDamage(damage);
            }
        }

        private void ProcessRaycastNPC()
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
            {
                CreateHitImpact(hit);
                NpcHealth target = hit.transform.GetComponent<NpcHealth>();
                if (target == null) return;
                target.TakeDamage(damage);
            }
        }

        private void CreateHitImpact(RaycastHit hit)
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // Destroy (impact, .1f);
            LeanPool.Despawn(impact, .1f);
        }
    }
}
