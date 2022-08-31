using UnityEngine;
using UnityEngine.Pool;

namespace FPS_Prototype.Weapons
{
    [RequireComponent(typeof(WeaponProjectile))]
    public class AmmoMagazineProjectile : AmmoMagazine
    {
        #region Variables

        [SerializeField] GameObject projectilePrefab;

        ObjectPool<GameObject> pool;
        [SerializeField] Transform objectPoolParent;
        [SerializeField] int maxPoolSize = 10;
        [SerializeField] int defaultCapacity = 2;

        public GameObject GetObjectFromPool() => pool.Get();


        #endregion

        #region Default Methods
        private void Start()
        {
            CreateObjectPool();
        }

        #endregion

        #region Unique Methods

        void CreateObjectPool()
        {
            pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                false,
                defaultCapacity,
                maxPoolSize);
        }
        GameObject CreateObject()
        {
            GameObject createdObject = Instantiate(projectilePrefab, objectPoolParent);
            bool isProjectile = createdObject.TryGetComponent(out Projectile projectileScript);
            if (isProjectile)
            {
                projectileScript.AssignValues(pool, PiercesArmor, Damage);

            }
            else
            {
                Debug.Log("No projectile script attached!");
            }

            return createdObject;
        }

        #endregion
    }

}