using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

namespace As_Your_Last_Day
{
    public class PlayVideo : MonoBehaviour
    {
        private VideoPlayer _video;
        private void Start()
        {
            _video = GetComponent<VideoPlayer>();
            _video.Play();
            _video.loopPointReached += SkipVideo;
        }

        private void SkipVideo (VideoPlayer vp)
        {
            SceneManager.LoadScene(2);
        }
    }
}
