using UnityEngine;
using FPS_Prototype.UI;
using FPS_Prototype.General;

namespace FPS_Prototype.Weapons
{
    [RequireComponent(typeof(AmmoMagazine))]

    public class Weapon : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        int slotOccupied;
        float afterSwapShootingPreventionTimer;
        float cooldownBeforeNextShot;


        [Header("Components")]    /********/
        [SerializeField] WeaponData weaponData;
        [SerializeField] Animator anim;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AmmoMagazine ammo;
        [SerializeField] Reload reload;
        public WeaponData Data => weaponData;
        public Reload Reload => reload;
        public virtual void Activate() { return; }

        public AmmoMagazine Ammo
        {
            get => ammo;
            set => ammo = value;
        }

        #endregion

        #region Unique Methods

        public void Trigger()
        {
            bool canShoot = (Time.time > afterSwapShootingPreventionTimer && !reload.ActionStarted);

            if (canShoot)
            {
                AttemptToFire();
            }
        }

        private void AttemptToFire()
        {
            bool readyToFire = Time.time > cooldownBeforeNextShot;
            if (readyToFire)
            {
                if (HasEnoughAmmoLoaded())
                {
                    FireBullet();
                }
                else
                {
                    reload.Begin();
                }
            }
        }

        private bool HasEnoughAmmoLoaded()
        {
            if (ammo.Take())
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void FireBullet()
        {
            cooldownBeforeNextShot = Time.time + weaponData.fireRate;
            audioSource.Play();
            anim.Play("fire", -1, -1);
            Activate();
        }


        public void AssignToSlot(int slot)
        {
            slotOccupied = slot;
            UIevents.PlayWeaponSlotIconAnimation(ConstantValues.WeaponSelectAnimationString, slotOccupied);
            afterSwapShootingPreventionTimer = Time.time + weaponData.afterSwapFireCooldown;
        }

        public void Unselect()
        {
            UIevents.PlayWeaponSlotIconAnimation(ConstantValues.WeaponUnSelectAnimationString, slotOccupied);
            gameObject.SetActive(false);
        }

        #endregion
    }
}