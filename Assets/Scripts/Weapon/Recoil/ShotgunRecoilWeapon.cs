using System.Collections;
using UnityEngine;

namespace As_Your_Last_Day.Weapon.Recoil
{
    public class ShotgunRecoilWeapon : MonoBehaviour
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
            weapon.GetComponent<Animator>().Play("Shotgun");
            yield return new WaitForSeconds(.1f);
            weapon.GetComponent<Animator>().Play("New State");
        }
    }
}

