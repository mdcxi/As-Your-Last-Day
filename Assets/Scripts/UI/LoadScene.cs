using As_Your_Last_Day.Weapon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace As_Your_Last_Day.UI
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private GameObject optionExitCanvas;
        [SerializeField] private int sceneIndex = 0;
        
        private void Start() 
        {
            optionExitCanvas.SetActive(false);
        }
        
        public void Load_Gameplay_Scene ()
        {
            SceneManager.LoadScene(sceneIndex);
        }
    
        public void OpenExitCanvasAtHomeScene ()
        {     
            optionExitCanvas.SetActive(true);
        }
    
        public void CloseExitCanvasAtHomeScene ()
        {
            optionExitCanvas.SetActive(false);
        }
        public void OpenExitCanvas ()
        {     
            Time.timeScale = 0;
            FindObjectOfType<WeaponSwitcher>().enabled = false;
            FindObjectOfType<Weapons>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            optionExitCanvas.SetActive(true);
        }
    
        public void Exit_Scene ()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    
        public void CloseExitCanvas ()
        {
            Time.timeScale = 1;
            FindObjectOfType<SceneLoader>().BackToGamePlay();
            FindObjectOfType<Weapons>().enabled = true;
            optionExitCanvas.SetActive(false);
        }
    }
}

