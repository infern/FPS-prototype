using UnityEngine;
using FPS_Prototype.Objects;
using FPS_Prototype.UI;


namespace FPS_Prototype.Weapons
{
    public class AmmoMagazine : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]    /********/
        [SerializeField] int maxAmmo;
        [SerializeField] ObjectInfo.MaterialType piercedArmorTypes;
        [SerializeField] [Range(10f, 100f)] float damage;

        public ObjectInfo.MaterialType PiercesArmor => piercedArmorTypes;
        public float Damage => damage;




        [Header("Data")]    /********/
        [SerializeField]
        int currentAmmo;
        public Vector2 AmmoCount => new Vector2(currentAmmo, maxAmmo);



        #endregion


        #region Base Methods

        void OnEnable()
        {
            UIevents.UpdateAmmoInformation(currentAmmo, maxAmmo);
        }

        void Awake()
        {
            currentAmmo = maxAmmo;
        }

        #endregion


        #region Unique Methods

        public bool TryToDepleteSingleBullet()
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                UIevents.AmmoDeplated(currentAmmo);
                return true;
            }
            else
            {
                return false;
            }
        }


        public void LoadMax()
        {
            currentAmmo = maxAmmo;
            UIevents.AmmoDeplated(currentAmmo);
        }


        #endregion


    }

}