using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace As_Your_Last_Day
{
    public class NpcTalk : MonoBehaviour
    {
        [SerializeField] private AudioSource npcTalkAudio;
        [SerializeField] private GameObject npcBorder;
        
        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TalkToPlayer();
                // Destroy(npcBorder);
                LeanPool.Despawn(npcBorder);
            }
        }

        private void TalkToPlayer()
        {
            npcTalkAudio.Play();
            GetComponent<Animator>().SetBool("greet", true);
        }
    }
}

