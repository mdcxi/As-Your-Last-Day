using UnityEngine;

namespace As_Your_Last_Day.Weapon
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private int currentWeapon = 0;
        [SerializeField] private AudioSource weaponSwitchAudio;
        
        private void Start()
        {
            SetWeaponActive();
        }

        private void Update()
        {
            int previousWeapon = currentWeapon;

            ProcessKeyInput();
            ProcessScrollWheel();

            if (previousWeapon != currentWeapon)
            {
                SetWeaponActive();
            }
        }

        private void ProcessKeyInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon = 0;
                weaponSwitchAudio.Play();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeapon = 1;
                weaponSwitchAudio.Play();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentWeapon = 2;
                weaponSwitchAudio.Play();
            }
        }

        private void ProcessScrollWheel()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (currentWeapon >= transform.childCount - 1)
                {
                    currentWeapon = 0;
                    weaponSwitchAudio.Play();
                }
                else
                {
                    currentWeapon++;
                    weaponSwitchAudio.Play();
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (currentWeapon <= 0)
                {
                    currentWeapon = transform.childCount - 1;
                    weaponSwitchAudio.Play();
                }
                else
                {
                    currentWeapon--;
                    weaponSwitchAudio.Play();
                }
            }
        }

        private void SetWeaponActive()
        {
            int weaponIndex = 0;

            foreach (Transform weapon in transform)
            {
               if (weaponIndex == currentWeapon)
               {
                   weapon.gameObject.SetActive(true);
               } 
               else
               {
                   weapon.gameObject.SetActive(false);
               } 
               weaponIndex++;
            }
        }
    }
}

