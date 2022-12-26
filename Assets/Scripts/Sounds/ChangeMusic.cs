using UnityEngine;
using UnityEngine.UI;

namespace As_Your_Last_Day.Sounds
{
    public class ChangeMusic : MonoBehaviour
    {
        [SerializeField] private Sprite onSound;
        [SerializeField] private Sprite offSound;
        [SerializeField] private Button soundButton;

        public void ChangeMusicSound()
        {
            if (soundButton.image.sprite == onSound)    
            {
                Debug.Log("Turn on");
                soundButton.image.sprite = offSound;
                AudioListener.pause = true;
            }
            else
            {
                Debug.Log("Turn off");
                soundButton.image.sprite = onSound;
                AudioListener.pause = false;
            }
        }
    }
}

