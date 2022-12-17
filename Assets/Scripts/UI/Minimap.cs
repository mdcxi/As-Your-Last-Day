using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace As_Your_Last_Day.UI
{
    public class Minimap : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private void LateUpdate()
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}

