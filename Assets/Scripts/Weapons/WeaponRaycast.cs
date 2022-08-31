using System.Collections;
using UnityEngine;
using FPS_Prototype.Objects;

namespace FPS_Prototype.Weapons
{
    public class WeaponRaycast : Weapon
    {
        #region Variables

        [Header("Settings")]    /********/
        [SerializeField] [Range(30f, 500f)] float raycastRange = 250f;


        [Header("Data")]    /********/
        private Camera cam;
        AmmoMagazineRaycast rayCastAmmoMagazine;
        Coroutine Co_LaserEffect;


        [Header("Components")]    /********/
        [SerializeField]
        public Transform gunEnd;
        [SerializeField]
        private LineRenderer laserLine;

        #endregion


        #region Default Methods
        void OnDisable()
        {
            StopCurrentCoroutine();
        }

        void Start()
        {
            AssingStartingValues();
        }

        #endregion


        #region Unique Methods

        private void AssingStartingValues()
        {
            cam = Camera.main;
            bool rayCastAmmoAttached = Ammo.TryGetComponent<AmmoMagazineRaycast>(out rayCastAmmoMagazine);
            if (rayCastAmmoAttached)
            {
                rayCastAmmoMagazine.AssignLaserValues(laserLine);
            }
            else
            {
                Debug.LogError("Raycast ammo is not attached to Raycast weapon!", this.gameObject);
            }
        }

        public override void Activate()
        {
            StartLaserEffectCoroutine();
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, raycastRange))
            {
                laserLine.SetPosition(1, hit.point);
                IDamageable health = hit.collider.GetComponent<IDamageable>();

                if (health != null)
                {
                    health.DamageCheck(Ammo.Damage, rayCastAmmoMagazine.PiercesArmor);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (cam.transform.forward * raycastRange));
            }
        }

        private IEnumerator LaserEffect()
        {
            float laserDuration = rayCastAmmoMagazine.LaserDuration;
            float lasorWidth = rayCastAmmoMagazine.LaserWidth;
            laserLine.enabled = true;
            float totalTime = 0;
            while (totalTime <= laserDuration)
            {
                float width = lasorWidth * (lasorWidth - (totalTime / laserDuration));
                laserLine.startWidth = width;
                laserLine.endWidth = width;
                totalTime += Time.deltaTime;
                yield return null;
            }

            laserLine.enabled = false;
        }

        void StartLaserEffectCoroutine()
        {
            StopCurrentCoroutine();
            Co_LaserEffect = StartCoroutine(LaserEffect());
        }

        private void StopCurrentCoroutine()
        {
            if (Co_LaserEffect != null)
            {
                StopCoroutine(Co_LaserEffect);
                Co_LaserEffect = null;
                laserLine.enabled = false;
            }
        }

        #endregion
    }



}