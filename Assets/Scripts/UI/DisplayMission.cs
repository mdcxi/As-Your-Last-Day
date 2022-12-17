using UnityEngine;
using UnityEngine.SceneManagement;

namespace As_Your_Last_Day.UI
{
    public class DisplayMission : MonoBehaviour
    {
        public void ViewMission()
        {
            LoadMissionScene();
        }

        private void LoadMissionScene()
        {
            Debug.Log("Load Mission Scene");
            Time.timeScale = 1;
            enabled = false;
            SceneManager.LoadScene(3);
        }
    }
}

