using System;
using As_Your_Last_Day.Weapon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace As_Your_Last_Day.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] int sceneIndex = 0;
        [SerializeField] GameObject buttonCanvas;
        [SerializeField] GameObject menuButton;    
    
        private void Start () 
        {
            buttonCanvas.gameObject.SetActive(false); 
        }
        

        public void ReloadGame()
        {
            Debug.Log("Reload Game");
            SceneManager.LoadScene(sceneIndex);
            UnlockState();
        }
    
        public void BackToHome ()
        {
            Debug.Log("Back to home");
            SceneManager.LoadScene(0);
    
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            enabled = false;
        }
    
        public void ExitGame ()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    
        public void DisplayButtonCanvas ()
        {  
            Debug.Log("Display button canvas");
            
            // buttonCanvas.enabled = true;
            buttonCanvas.gameObject.SetActive(true);
            LockState();
            menuButton.SetActive(false);
        }
    
        public void BackToGamePlay ()
        {
            UnlockState();
     
            // buttonCanvas.enabled = false;
            buttonCanvas.gameObject.SetActive(false);
            menuButton.SetActive(true);
        }
    
        public void UnlockState ()
        {
            Time.timeScale = 1;
            FindObjectOfType<WeaponSwitcher>().enabled = true;
            FindObjectOfType<Weapons>().enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible =  false;
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

