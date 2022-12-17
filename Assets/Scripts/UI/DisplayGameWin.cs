using As_Your_Last_Day.Sounds;
using As_Your_Last_Day.Weapon;
using UnityEngine;

namespace As_Your_Last_Day.UI
{
    public class DisplayGameWin : MonoBehaviour
    {
        [SerializeField] private Canvas gameWinCanvas;
        [SerializeField] private AudioSource gameWinClip;
        [SerializeField] private GameObject backgroundClip;
        
        private void Start()
        {
            gameWinCanvas.enabled = false;
        }

        public void HandleWin()
        {
            gameWinClip.Play();
            backgroundClip.GetComponent<BackgroundMusic>().backgroundAudio.Stop();       
            gameWinCanvas.enabled = true;
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

