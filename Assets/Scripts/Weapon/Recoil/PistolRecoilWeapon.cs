using System.Collections;
using UnityEngine;

namespace As_Your_Last_Day.Weapon.Recoil
{
    public class PistolRecoilWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject weapon;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(StartRecoil());
            }
        }

        private IEnumerator StartRecoil()
        {
            weapon.GetComponent<Animator>().Play("Piston");
            yield return new WaitForSeconds(0.2f);
            weapon.GetComponent<Animator>().Play("New State");
        }
    }
}

