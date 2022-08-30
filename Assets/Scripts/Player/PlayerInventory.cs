using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS_Prototype.Weapons;
using FPS_Prototype.UI;

namespace FPS_Prototype.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Settings")]    /********/
        [SerializeField]
        float switchCooldown = 0.3f;

        [Header("Data")]    /********/
        int selectionID = -1;
        float switchTimer;

        [Header("Components")]    /********/
        [SerializeField]
        PlayerComponents player;
        [SerializeField] List<Weapon> allWeapons = new List<Weapon>();
        [SerializeField] Weapon equippedWeapon;
        [SerializeField] Weapon previousWeapon;



        void Awake()
        {
            StartCoroutine(EquipStarttingWeapon());
        }


        public void EquipWeapon(int slot)
        {
            bool swiitchReady = Time.time > switchTimer;
            bool notTheSameWeapon = selectionID != slot;
            if (swiitchReady && notTheSameWeapon)
            {
                switchTimer = Time.time + switchCooldown;
                if (previousWeapon != null)
                {
                    previousWeapon.Unselect();
                }

                selectionID = slot;
                AssignWeaponValues();
            }
        }

        private void AssignWeaponValues()
        {
            equippedWeapon = allWeapons[selectionID];
            previousWeapon = equippedWeapon;
            equippedWeapon.gameObject.SetActive(true);
            equippedWeapon.Select(selectionID);
            WeaponData data = equippedWeapon.Data;
            UIevents.ChangeWeaponText(data);
        }

        public Weapon EquippedWeapon()
        {
            return equippedWeapon;
        }

        IEnumerator EquipStarttingWeapon()
        {
            yield return new WaitForSeconds(0.2f);
            EquipWeapon(0);

        }

        public void StartReloading()
        {
            equippedWeapon.Reload.Begin();
        }
        public void MouseScrollSwitch(float a)
        {
            int value = a > 0 ? 1 : -1;
            int nextSelection = selectionID;
            nextSelection += value;
            if (nextSelection > allWeapons.Count - 1)
            {
                nextSelection = 0;
            }
            else if (nextSelection < 0)
            {
                nextSelection = allWeapons.Count - 1;
            }
            EquipWeapon(nextSelection);


        }

    }

}