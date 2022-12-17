using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace As_Your_Last_Day.Weapon
{
    public class WeaponZoom : MonoBehaviour
    {
        [SerializeField] private Camera fpsCamera;
        [SerializeField] private float zoomedOutFOV = 60f;
        [SerializeField] private float zoomedInFOV = 20f; 
        [SerializeField] private float zoomOutSensitivity = 2f;
        [SerializeField] private float zoomInSensitivity = .5f;
        
        [SerializeField] private RigidbodyFirstPersonController fpsController;
        
        bool _zoomedInToggle = false;
    
        private void OnDisable() 
        {
            ZoomOut();   
        }
    
        private void Update() 
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (_zoomedInToggle == false)
                {
                    ZoomIn();
                }
                else
                {
                    ZoomOut();
                }
            }
        }
    
        private void ZoomIn()
        {
            _zoomedInToggle = true;
            fpsCamera.fieldOfView = zoomedInFOV;
            fpsController.mouseLook.XSensitivity = zoomInSensitivity;
            fpsController.mouseLook.YSensitivity = zoomInSensitivity;
        }
    
        private void ZoomOut()
        {
            _zoomedInToggle = false;
            fpsCamera.fieldOfView = zoomedOutFOV;
            fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
            fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
        }
    }
}

