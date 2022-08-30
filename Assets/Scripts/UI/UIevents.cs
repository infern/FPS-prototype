using UnityEngine.Events;
using UnityEngine;
using FPS_Prototype.Weapons;

namespace FPS_Prototype.UI
{
    public class UIevents : MonoBehaviour
    {
        public static event UnityAction<WeaponData> ChangeWeaponTextTrigger;
        public static void ChangeWeaponText(WeaponData _data) => ChangeWeaponTextTrigger?.Invoke(_data);


        public static event UnityAction<int> AmmoDeplatedTrigger;
        public static void AmmoDeplated(int currentAmmo) => AmmoDeplatedTrigger?.Invoke(currentAmmo);


        public static event UnityAction<int, int> UpdateAmmoInformationTrigger;
        public static void UpdateAmmoInformation(int currentAmmo, int maxAmmo) => UpdateAmmoInformationTrigger?.Invoke(currentAmmo, maxAmmo);


        public static event UnityAction<string, int> WeaponIconAnimationTrigger;
        public static void PlayWeaponSlotIconAnimation(string animationName, int animatorSlotIndex) => WeaponIconAnimationTrigger?.Invoke(animationName, animatorSlotIndex);

    }

}