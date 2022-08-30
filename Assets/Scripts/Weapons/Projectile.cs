using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using FPS_Prototype.Objects;

namespace FPS_Prototype.Weapons
{
    public class Projectile : MonoBehaviour
    {
        #region Variables


        [Header("Settings")]    /********/
        [SerializeField]
        [Range(0.1f, 7f)]
        float disappearDuration = 7f;
        [SerializeField]
        [Range(1f, 7f)]
        float particleEffectDuration = 7f;

        [Header("Data")]    /********/
        float damage;
        bool targetHit = false;
        ObjectPool<GameObject> pool;
        ObjectInfo.MaterialType piercedArmorTypes;
        Coroutine Co_Disappear;


        [Header("Components")]    /********/
        [SerializeField]
        GameObject model;
        [SerializeField]
        GameObject projectile;
        [SerializeField]
        AudioSource audioSource;
        [SerializeField]
        List<AudioClip> explosionSounds = new List<AudioClip>();
        [SerializeField]
        ParticleSystem explosionParticleSystem;
        [SerializeField]
        MeshRenderer mesh;
        [SerializeField]
        Rigidbody rb;
        #endregion


        #region Base Methods

        void OnEnable()
        {
            ResetValues();

        }

        private void ResetValues()
        {
            explosionParticleSystem.Clear();
            rb.velocity = Vector3.zero;
            targetHit = false;
            model.SetActive(true);
            projectile.SetActive(false);
            StartDisappearCoroutine(disappearDuration);
        }


        #endregion

        #region Unique Methods

        IEnumerator DisappearTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
            pool.Release(this.gameObject);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!targetHit)
            {
                targetHit = true;
                rb.velocity = Vector3.zero;
                model.SetActive(false);
                projectile.SetActive(true);
                StartDisappearCoroutine(particleEffectDuration);
                int randomSound = Random.Range(0, explosionSounds.Capacity);
                audioSource.clip = explosionSounds[randomSound];
                audioSource.Play();
                IDamageable target = collision.gameObject.GetComponent<IDamageable>();
                bool validTarget = (target != null);
                if (validTarget) target.DamageCheck(damage, piercedArmorTypes);
            }
        }

        private void StartDisappearCoroutine(float duration)
        {
            if (Co_Disappear != null)
            {
                StopCoroutine(Co_Disappear);
                Co_Disappear = null;
            }
            Co_Disappear = StartCoroutine(DisappearTimer(duration));
        }

        public void AssignValues(ObjectPool<GameObject> _pool, ObjectInfo.MaterialType _piercedArmorTypes, float _damage)
        {
            pool = _pool;
            piercedArmorTypes = _piercedArmorTypes;
            damage = _damage;
            ParticleSystem.MainModule gm = explosionParticleSystem.main;
            gm.startColor = mesh.material.color;
        }
        #endregion
    }

}