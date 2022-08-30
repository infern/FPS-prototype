using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FPS_Prototype.Weapons;

namespace FPS_Prototype.UI
{
    public class UIweaponInventory : MonoBehaviour
    {
        [SerializeField] List<Animator> weaponSlotIconAnimatorList = new List<Animator>();

        [SerializeField] TextMeshProUGUI weaponNameTextMeshPro;
        [SerializeField] TextMeshProUGUI damageTextMeshPro;
        [SerializeField] TextMeshProUGUI currentAmmoTextMeshPro;
        [SerializeField] TextMeshProUGUI maxAmmoTextMeshPro;



        private void OnEnable()
        {
            UIevents.ChangeWeaponTextTrigger += ChangeWeaponInfoText;
            UIevents.UpdateAmmoInformationTrigger += ChangeAmmoText;
            UIevents.AmmoDeplatedTrigger += UpdateCurrentAmmoCount;
            UIevents.WeaponIconAnimationTrigger += PlayWeaponIconAnimation;



        }

        private void OnDisable()
        {
            UIevents.ChangeWeaponTextTrigger -= ChangeWeaponInfoText;
            UIevents.UpdateAmmoInformationTrigger -= ChangeAmmoText;
            UIevents.AmmoDeplatedTrigger -= UpdateCurrentAmmoCount;
            UIevents.WeaponIconAnimationTrigger -= PlayWeaponIconAnimation;

        }


        public void ChangeWeaponInfoText(WeaponData data)
        {
            weaponNameTextMeshPro.text = data.weaponName;
            damageTextMeshPro.text = data.infoDamage;
        }

        public void PlayWeaponIconAnimation(string animationName)
        {
            weaponSlotIconAnimatorList[0].Play(animationName);
        }


        void ChangeAmmoText(int currentAmmo, int maxAmmo)
        {
            currentAmmoTextMeshPro.text = currentAmmo.ToString();
            maxAmmoTextMeshPro.text = maxAmmo.ToString();
        }

        void UpdateCurrentAmmoCount(int count)
        {
            currentAmmoTextMeshPro.text = count.ToString();
        }

        void PlayWeaponIconAnimation(string animationName, int animatorSlotIndex)
        {
            if (weaponSlotIconAnimatorList[animatorSlotIndex] != null)
            {
                weaponSlotIconAnimatorList[animatorSlotIndex].Play(animationName);
            }
        }

    }

}