using As_Your_Last_Day.Sounds;
using As_Your_Last_Day.Weapon;
using UnityEngine;

namespace As_Your_Last_Day.UI
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private AudioSource gameOverClip;
        [SerializeField] private GameObject backgroundClip;
        
        private void Start()
        {
            gameOverCanvas.enabled = false;
        }

        public void HandleDeath()
        {
            gameOverClip.Play();
            backgroundClip.GetComponent<BackgroundMusic>().backgroundAudio.Stop();
            gameOverCanvas.enabled = true;
            LockState();
        }

        public void LockState ()
        {
            Time.timeScale = 0;
            FindObjectOfType<Weapons>().enabled = false;
            FindObjectOfType<WeaponSwitcher>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

