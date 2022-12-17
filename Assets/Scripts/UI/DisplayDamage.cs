using System.Collections;
using UnityEngine;

namespace As_Your_Last_Day.UI
{
    public class DisplayDamage : MonoBehaviour
    {
        [SerializeField] private Canvas impactCanvas;
        [SerializeField] private float impactTime = 0.3f;
        [SerializeField] private AudioSource playerHurt;
        
        private void Start()
        {
            impactCanvas.enabled = false;
        }

        public void ShowDamageImpact()
        {
            StartCoroutine(ShowSplatter());
        }

        private IEnumerator ShowSplatter()
        {
            impactCanvas.enabled = true;
            playerHurt.Play();
            yield return new WaitForSeconds(impactTime);
            impactCanvas.enabled = false;
            playerHurt.Stop();
        }
    }
}

