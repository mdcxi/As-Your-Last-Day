using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace As_Your_Last_Day
{
    public class VFXHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bloodVFX;

        private void Start()
        {
            bloodVFX = GetComponent<ParticleSystem>();
        }

        public void PlayBloodVFX ()
        {
            bloodVFX.Play();
        }
    }
}

