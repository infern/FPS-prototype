using UnityEngine;

namespace FPS_Prototype.Weapons
{
    public class WeaponProjectile : Weapon
    {

        [Header("Settings")]    /********/
        [SerializeField] [Range(50f, 400f)] float projectileSpeed = 300f;


        [Header("Data")]    /********/
        AmmoMagazineProjectile projectileAmmoMagazine;


        #region Default Methods
        void Start()
        {
            AssingStartingValues();
        }

        #endregion

        #region Unique Methods
        private void AssingStartingValues()
        {
            bool projectileAmmoAttached = Ammo.TryGetComponent(out projectileAmmoMagazine);
            if (!projectileAmmoAttached)
            {
                Debug.LogError("Projectile ammo is not attached to Projectile weapon!", this.gameObject);
            }
        }



        public override void Activate()
        {
            GameObject temp = projectileAmmoMagazine.GetObjectFromPool();
            temp.SetActive(true);
            temp.transform.position = transform.parent.position;
            temp.transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed, ForceMode.Impulse);
        }

        #endregion

    }

}