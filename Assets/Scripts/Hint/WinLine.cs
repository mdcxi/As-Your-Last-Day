using As_Your_Last_Day.UI;
using UnityEngine;

namespace As_Your_Last_Day
{
    public class WinLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) 
        {   
            if (other.gameObject.CompareTag("Hint"))
            {
                Destroy(other.gameObject);
                GetComponent<DisplayGameWin>().HandleWin();
            } 
        }
    }
}

