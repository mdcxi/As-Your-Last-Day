using System;
using UnityEngine;

namespace As_Your_Last_Day
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] private AmmoSlot[] ammoSlots;

        [Serializable]
        private class AmmoSlot 
        {
            public AmmoType ammoType;
            public int ammoAmount;
        }

        public int GetCurrentAmmo(AmmoType ammoType)
        {
            return GetAmmoSlot(ammoType).ammoAmount;
        }

        public void ReduceCurrentAmmo(AmmoType ammoType)
        {
            GetAmmoSlot(ammoType).ammoAmount--;
        }

        public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
        {
            GetAmmoSlot(ammoType).ammoAmount+= ammoAmount;
        }

        private AmmoSlot GetAmmoSlot(AmmoType ammoType)
        {
            foreach (AmmoSlot slot in ammoSlots)
            {
                if (slot.ammoType == ammoType)
                {
                    return slot;
                }
            }
            return null;
        }
    }
}

