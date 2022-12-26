using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace As_Your_Last_Day.Sounds
{
    public class ItemSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource pickupSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                pickupSound.Play();   
            }
        }
    }

}
