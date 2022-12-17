using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace As_Your_Last_Day.UI
{
    public class EnemyDisplayScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyDiedText;
        private int _enemyDiedQuantity;

        private void Start()
        {
            _enemyDiedQuantity = 0;
        }

        public void DisplayZombieDiedCanvas ()
        {
            _enemyDiedQuantity++;
            enemyDiedText.text = _enemyDiedQuantity.ToString();     
        }
    }
}

