using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using FPS_Prototype.General;

namespace FPS_Prototype.Objects
{
    public class DestructibleObject : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("Settings")]    /********/
        [SerializeField]
        ObjectInfo.MaterialType materialType;
        [SerializeField]
        [Range(25, 1000f)]
        float maxHealth;
        [SerializeField]
        [Range(0.1f, 3f)]
        float disappearDuration = 1f;
        [SerializeField]
        UnityEvent onDestroyActions;


        [Header("Data")]    /********/
        bool alive = true;
        float currentHealth;
        Camera mainCamera;

        [Header("Components")]    /********/
        [SerializeField]
        Collider col;
        [SerializeField]
        GameObject textParent;
        [SerializeField]
        TextMeshPro materialTmp;
        [SerializeField]
        TextMeshPro healthTmp;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip destroyedSfx;

        #endregion

        #region Base Methods
        void Start()
        {
            AssignStartingValues();
        }

        private void AssignStartingValues()
        {
            materialTmp.text = materialType.ToString();
            currentHealth = maxHealth;
            healthTmp.text = currentHealth.ToString();
            mainCamera = Camera.main;
        }

        void OnBecameVisible()
        {
            textParent.SetActive(true);
        }

        void OnBecameInvisible()
        {
            textParent.SetActive(false);
        }

        void LateUpdate()
        {
            RotateText();
        }

        #endregion

        #region Unique Methods

        public void DamageCheck(float value, ObjectInfo.MaterialType projectileType)
        {
            bool materialMatching = (projectileType & materialType) == materialType;

            if (alive && materialMatching)
            {
                ReceiveDamage(value);
            }
        }

        private void ReceiveDamage(float damage)
        {
            currentHealth -= damage;
            healthTmp.text = currentHealth.ToString();
            if (currentHealth <= 0)
            {
                Destroyed();
            }
            else
            {
                audioSource.Play();

            }
        }

        void Destroyed()
        {
            onDestroyActions.Invoke();
        }

        public void Disappear()
        {
            alive = false;
            audioSource.clip = destroyedSfx;
            audioSource.Play();
            col.enabled = false;
            textParent.SetActive(false);
            StatsEventManager.ObjectDestroyed();
            StartCoroutine(ScaleOverTime(disappearDuration));
        }

        void RotateText()
        {
            if (textParent.activeInHierarchy)
            {
                materialTmp.transform.LookAt(mainCamera.transform);
                materialTmp.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
            }
        }
        IEnumerator ScaleOverTime(float time)
        {
            Vector3 originalScale = transform.localScale;
            float currentTime = 0.0f;
            do
            {
                transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, currentTime / time);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= time);

            Destroy(gameObject);
        }

        public ObjectInfo.MaterialType GetMaterial()
        {
            return materialType;
        }

        #endregion

    }

}